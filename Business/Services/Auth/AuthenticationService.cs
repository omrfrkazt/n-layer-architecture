using Business.Helper;
using Business.Keys;
using Business.ModelExtensions.User;
using Business.Services.Cache;
using Business.Services.Token;
using Business.Services.User;
using DataAccess.Services.User;
using Dto.Common;
using Dto.Common.BaseResponse;
using Dto.Models.Auth;
using Dto.Models.Permissions;
using Dto.Models.User;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Auth
{

    public class AuthenticationService : IAuthenticationService
    {
        #region Fields&Ctor

        private readonly IUserDal _userDal;
        private readonly ITokenService _tokenService;
        private readonly ICacheService _staticCache;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationService(IUserDal userDal,
        ITokenService tokenService,
        ICacheService staticCache,
        ILogger<AuthenticationService> logger,
        IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _tokenService = tokenService;
            _staticCache = staticCache;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        #region Methods
        public async Task<BaseResponseModel> LoginAsync(LoginModel loginModel)
        {
            var response = new BaseResponseModel();

            var user = await _userDal.GetAsync(a=> a.Email == loginModel.Email);

            if (user == null || !user.IsActive)
            {
                response.IsSuccess = false;
                response.Message = "";
                response.StatusCode = StatusCodes.Status401Unauthorized;
                return response;
            }

            if (!HashHelper.VerifyPasswordHash(loginModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Wrong Password";
                return response;
            }
            var token = _tokenService.CreateToken(user);
            var refreshtoken = _tokenService.CreateRefreshToken(user);
            user.RefreshToken = refreshtoken.Token;
            user.RefreshTokenExpireDate = refreshtoken.Expires;
            _userDal.Update(user);

            var returnModel = user.ToUserLoginModel();
            returnModel.Token = token.Token;
            returnModel.RefreshToken = returnModel.Token;
            returnModel.ForcePasswordChange = returnModel.ForcePasswordChange;
            response.IsSuccess = true;
            response.Data = returnModel;
            response.StatusCode = StatusCodes.Status200OK;
            return response;
        }

        public Task<BaseResponseModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel> ForgotPasswordAsync(ForgotPasswordModel ForgotPasswordModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel> ForgotPasswordConfirmAsync(ForgotPasswordConfirmModel refreshTokenModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel> Me()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel> RefreshTokenAsync(RefreshTokenModel refreshTokenModel)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
