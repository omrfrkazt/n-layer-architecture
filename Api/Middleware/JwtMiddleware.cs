using Business;
using Business.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IUserService userBusiness)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, userBusiness, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserService userBusiness, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var personelId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                context.Items["User"] = await userBusiness.GetByIdForTokenAsync(personelId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
        }
    }

}
