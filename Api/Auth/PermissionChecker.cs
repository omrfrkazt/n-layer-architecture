using Business.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Auth
{
    public static class PermissionChecker
    {
        public static bool ThisPermissionIsAllowed(this string packedPermission, string permissionName)
        {
            var usersPermission = packedPermission.UnPackPermissionsFromString().ToArray();

            if (!Enum.TryParse(permissionName, true, out Permissions permissionToCheck))
            {
                throw new InvalidEnumArgumentException();
            }
            return usersPermission.UserHasPermission(permissionToCheck);
        }
        public static bool UserHasPermission(this Permissions[] usersPermission, Permissions permissionToCheck)
        {
            return usersPermission.Contains(permissionToCheck) || usersPermission.Contains(Permissions.AccessAll);
        }
    }
}
