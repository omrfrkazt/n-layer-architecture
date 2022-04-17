using Dto.Models.Auth;
using Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Token
{
   public class JwtTokenService : ITokenService
    {
        #region Fields&Ctor

        private readonly AppSettings _appSettings;
        public JwtTokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        #endregion

        #region Methods
        public TokenModel CreateToken(GenUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            DateTime expiresDate = DateTime.Now.AddMinutes(_appSettings.TokenExpiresAddMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {

                    new Claim("id" , user.Id.ToString()),

                }),
                Expires = expiresDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToken = new TokenModel()
            {
                Token = tokenHandler.WriteToken(token),
                Expires = expiresDate
            };

            return userToken;
        }
        public TokenModel CreateRefreshToken(GenUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.RefreshSecret);

            DateTime expiresDate = DateTime.Now.AddMinutes(_appSettings.RefreshTokenExpiresAddMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id" , user.Id.ToString())
                }),
                Expires = expiresDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToken = new TokenModel()
            {
                Token = tokenHandler.WriteToken(token),
                Expires = expiresDate
            };

            return userToken;
        }
        public TokenModel CreateForgotPasswordToken(GenUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            DateTime expiresDate = DateTime.Now.AddHours(24);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id" , user.Id.ToString())
                }),
                Expires = expiresDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToken = new TokenModel()
            {
                Token = tokenHandler.WriteToken(token),
                Expires = expiresDate
            };

            return userToken;
        }
        #endregion

    }
}
