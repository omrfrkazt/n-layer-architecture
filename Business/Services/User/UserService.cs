using Business.Helper;
using Business.Keys;
using Business.ModelExtensions.User;
using Business.Services.Cache;
using Business.Services.Roles;
using DataAccess.Services.User;
using Dto.Common;
using Dto.Common.BaseResponse;
using Dto.Models.User;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Services.User
{
    public class UserService : IUserService
    {
        #region Fields&Ctor
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService _staticCache;
        private readonly IUserDal _userDal;
        public UserService(ICacheService staticCache, IUserDal userDal)
        {
            _staticCache = staticCache;
            _userDal = userDal;
        }
        #endregion

        #region Method
        public async Task<UserModel> GetByIdAsync(int id)
        {
            if (id == 0)
                return null;

            var key = string.Format(CacheKeys.User, id);

            var user = await _staticCache.GetAsync(key, async () =>
            {
                return await _userDal.GetByIdAsync(id);
            });
            return user.ToModel();
        }
        #endregion
        public async Task<BaseResponseModel> DeleteAsync(int id)
        {
            var response = new BaseResponseModel();

            var personel = await GetByIdAsync(id);

            if (personel == null)
            {
                _logger.LogWarning($"Personel not found Id :{id} Func:DeleteAsync()");
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = MessageKeys.UserNotFound;
                return response;
            }

            _userDal.Delete(personel.ToEntity());

            response.IsSuccess = true;

            if (response.IsSuccess)
            {
                var key = string.Format(CacheKeys.User, id);
                _staticCache.Remove(key);
                _staticCache.Set(key, personel);
            }

            return response;
        }

        public async Task<ContextUser> GetByIdForTokenAsync(int id)
        {
            var user = await GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning($"Personel not found Id :{id} Func:GetByIdForTokenAsync()");
                return null;
            }

            var contextUser = new ContextUser()
            {
                Email = user.Email,
                Id = user.Id,
                RefreshToken = user.RefreshToken,
                FirstName = user.Name,
                LastName = user.Surname,
            };
            return contextUser;
        }

        public async Task<BaseResponseModel> AddAsync(UserModel model)
        {
            var response = new BaseResponseModel();
            HashHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;
            model.HashEmail = CreateEmailHash(model.Email);
            var personel = model;
            await _userDal.AddAsync(personel.ToEntity());
            response.IsSuccess = true;
            return response;
        }

        public async Task<BaseResponseModel> UpdateAsync(UserModel model)
        {
            var response = new BaseResponseModel();
            var personel = model;
            _userDal.Update(personel.ToEntity());
            response.IsSuccess = true;
            return response;
        }

        public async Task<BaseResponseModel> GetByEmailAsync(string email)
        {
            var response = new BaseResponseModel();
            var user = await _userDal.GetAsync(a=> a.Email == email);
            response.Data = user.ToModel();
            response.IsSuccess = true;
            response.Message = HttpStatusCode.OK.ToString();
            return response;
        }
        private byte[] CreateEmailHash(string email)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email));
        }
    }
}
