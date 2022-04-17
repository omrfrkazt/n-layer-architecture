using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper
{
    public static class PermissionPackers
    {
        public static string PackPermissionsIntoString(this IEnumerable<Permissions> permissions)
        {
            return permissions.Aggregate("", (s, permission) => s + (char)permission);
        }

        public static IEnumerable<Permissions> UnPackPermissionsFromString(this string packedPermissions)
        {
            if (packedPermissions == null)
                throw new ArgumentNullException("");

            foreach (var character in packedPermissions)
            {
                yield return ((Permissions)character);
            }
        }

    }


}
