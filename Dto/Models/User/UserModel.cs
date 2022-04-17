using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public int MyProperty { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }
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
