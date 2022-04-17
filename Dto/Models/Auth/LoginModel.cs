using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models.Auth
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginResponseModel
    {

        public int PersonelId { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Email { get; set; }


        public string Token { get; set; }


        public string RefreshToken { get; set; }


        public bool ForcePasswordChange { get; set; }

    }
}
