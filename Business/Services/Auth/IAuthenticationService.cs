using Dto.Common;
using Dto.Common.BaseResponse;
using Dto.Models.Auth;
using Dto.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IAuthenticationService
    {
        Task<BaseResponseModel> LoginAsync(LoginModel loginModel);


        Task<BaseResponseModel> RefreshTokenAsync(RefreshTokenModel refreshTokenModel);


        Task<BaseResponseModel> ForgotPasswordAsync(ForgotPasswordModel ForgotPasswordModel);


        Task<BaseResponseModel> ForgotPasswordConfirmAsync(ForgotPasswordConfirmModel refreshTokenModel);


        Task<BaseResponseModel> ChangePasswordAsync(ChangePasswordModel changePasswordModel);


        Task<BaseResponseModel> Me();
    }
}
