using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GenUser : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public int CreateUser { get; set; }
        public int UpdateUser { get; set; }
        public bool IsLogin { get; set; }
        #region Password&Token
        public byte[] HashEmail { get; set; }


        public byte[] PasswordHash { get; set; }


        public byte[] PasswordSalt { get; set; }


        public string RefreshToken { get; set; }


        public string PasswordToken { get; set; }


        public bool ForcePasswordChange { get; set; }


        public DateTime? RefreshTokenExpireDate { get; set; }
        #endregion
    }
}
