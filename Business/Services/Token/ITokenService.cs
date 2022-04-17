using Dto.Models.Auth;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Token
{
    public interface ITokenService
    {
        TokenModel CreateToken(GenUser user);
        TokenModel CreateRefreshToken(GenUser user);
        TokenModel CreateForgotPasswordToken(GenUser user);
    }
}
