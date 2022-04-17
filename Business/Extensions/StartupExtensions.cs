using Business.Services;
using Business.Services.Auth;
using Business.Services.Cache;
using Business.Services.Token;
using Business.Services.User;
using Core;
using Core.EntityFramework;
using DataAccess.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions
{
    public static class StartupExtensions
    {
        public static void AddBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new System.ArgumentNullException(nameof(configuration));
            }


            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserDal, UserDal>();
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

        }
        public static void AddProviderModule(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
            {
                throw new System.ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new System.ArgumentNullException(nameof(configuration));
            }
        }
    }
}
