using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.User
{
    public class GenRolesAuthories : BaseEntity
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }

        //isDelete-isWrite gibi ekstra permissionlar buraya yazilacak.
    }
}
