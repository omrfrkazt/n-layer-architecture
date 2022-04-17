using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Auth
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName ?? throw new ArgumentNullException(nameof(permissionName));
        }
        public string PermissionName { get;}
    }
}
