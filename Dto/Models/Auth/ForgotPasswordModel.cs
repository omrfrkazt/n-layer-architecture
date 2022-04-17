using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models.Auth
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordConfirmModel
    {
        public string Token { get; set; }

        public string Password { get; set; }

        public string PasswordRepeat { get; set; }
    }
}
