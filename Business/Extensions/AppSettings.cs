using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class AppSettings
    {
        public string Secret { get; set; }


        public string RefreshSecret { get; set; }


        public int TokenExpiresAddMinutes { get; set; }


        public int RefreshTokenExpiresAddMinutes { get; set; }


        public int CacheExpiresMunites { get; set; }
    }
}
