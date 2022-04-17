using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models.Permissions
{
    public class PermissionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
