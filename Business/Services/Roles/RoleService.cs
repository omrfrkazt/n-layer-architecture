using Dapper;
using DataAccess.Services.RoleGroups;
using DataAccess.Services.Roles;
using DataAccess.Services.UserRoles;
using Dto.Common;
using Dto.Common.BaseResponse;
using Dto.Models.Role;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Roles
{
    public class RoleService : IRoleService
    {
        #region Fields&Ctor
        private readonly IRoleDal _roleDal;
        private readonly IRoleGroupsDal _roleGroupsDal;
        private readonly IUserRolesDal _userRolesDal;
        public RoleService(IRoleDal roleDal, IRoleGroupsDal roleGroupsDal, IUserRolesDal userRolesDal)
        {
            _roleDal = roleDal;
            _roleGroupsDal = roleGroupsDal;
            _userRolesDal = userRolesDal;
        }
        #endregion
        public async Task<BaseResponseModel> GetRolesById(int userId)
        {
            return new BaseResponseModel();

        }

        public async Task<BaseResponseModel> GetRoleListByGroupId(int userId, int roleGroupID)
        {
            BaseResponseModel response = new();
          
            return response;
        }
    }
}
