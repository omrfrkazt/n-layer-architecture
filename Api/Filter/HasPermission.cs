using Business.Helper;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermission : AuthorizeAttribute
    {
        public HasPermission(Permissions permission) : base(permission.ToString())
        {

        }
    }
}
