using Core;
using Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Roles
{
    public interface IRoleDal : IEntityRepository<GenRolesAuthories>
    {
    }
}
