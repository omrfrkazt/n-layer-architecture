using Dto.Models.Auth;
using Dto.Models.User;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ModelExtensions.User
{
    public static class UserExtension
    {
        public static GenUser ToEntity(this UserModel model)
        {
            GenUser entity = new();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.Email = model.Email;
            entity.PasswordSalt = model.PasswordSalt;
            entity.PasswordHash = model.PasswordHash;
            entity.IsActive = model.IsActive;
            entity.CreatedDate = model.CreateDate;
            entity.UpdatedDate = model.UpdateDate;
            entity.CreateUser = model.CreateUserId;
            entity.UpdateUser = model.UpdateUserId;
            entity.RefreshToken = model.RefreshToken;
            return entity;
        }

        public static UserModel ToModel(this GenUser entity)
        {
            UserModel model = new();
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Surname = entity.Surname;
            model.Email = entity.Email;
            model.PasswordSalt = entity.PasswordSalt;
            entity.PasswordHash = model.PasswordHash;
            model.IsActive = entity.IsActive;
            model.CreateDate = entity.CreatedDate;
            model.UpdateDate = entity.UpdatedDate.Value;
            model.CreateUserId = entity.CreateUser;
            model.UpdateUserId = entity.UpdateUser;
            model.RefreshToken = entity.RefreshToken;
            return model;
        }
        public static LoginResponseModel ToUserLoginModel(this GenUser entity)
        {
            var model = new LoginResponseModel();
            model.PersonelId = entity.Id;
            model.Email = entity.Email;
            model.FirstName = entity.Name;
            model.LastName = entity.Surname;
            model.ForcePasswordChange = entity.ForcePasswordChange;
            return model;
        }
    }
}
