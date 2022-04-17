using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models.Auth
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }

        public string Password { get; set; }

        public string PasswordRepeat { get; set; }
    }
}
