using Dto.Common;
using Dto.Common.BaseResponse;
using Dto.Models.User;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User
{
    public interface IUserService
    {
        Task<UserModel> GetByIdAsync(int id);
        Task<BaseResponseModel> GetByEmailAsync(string email);
        Task<ContextUser> GetByIdForTokenAsync(int id);
        Task<BaseResponseModel> AddAsync(UserModel model);


        Task<BaseResponseModel> UpdateAsync(UserModel model);


        Task<BaseResponseModel> DeleteAsync(int id);

    }
}
