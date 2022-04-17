using Dto.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Common
{
    public class ContextUser
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Roles { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string RefreshToken { get; set; }
    }
}
