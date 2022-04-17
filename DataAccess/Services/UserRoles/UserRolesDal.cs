using Core.EntityFramework;
using Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.UserRoles
{
    public class UserRolesDal : EfRepositoryBase<GenRolesAndUsers, AppDbContext>, IUserRolesDal
    {
    }
}
