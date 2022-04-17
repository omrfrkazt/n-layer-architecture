using Business.Helper;
using Dto.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Auth
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        IHttpContextAccessor _httpContextAccessor;
        public PermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            int value = (int)Enum.Parse(typeof(Permissions), requirement.PermissionName);
            var permissionClaim = (ContextUser)_httpContextAccessor.HttpContext.Items["User"];
            if (permissionClaim.Roles.ThisPermissionIsAllowed(requirement.PermissionName))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
