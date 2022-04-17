using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models.Auth
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }

    }
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }

    }
}
