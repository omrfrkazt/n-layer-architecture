using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models.Role
{
    public class RoleModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int RoleGroupID { get; set; }
        public string GroupName { get; set; }
        public string RoleName { get; set; }
    }
}
