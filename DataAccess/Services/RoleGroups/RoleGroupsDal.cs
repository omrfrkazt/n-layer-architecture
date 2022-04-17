using Core.EntityFramework;
using Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.RoleGroups
{
    public class RoleGroupsDal : EfRepositoryBase<GenRoles, AppDbContext>, IRoleGroupsDal
    {
    }
}
