using Dto.Common;
using Dto.Common.BaseResponse;
using Dto.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Roles
{
    public interface IRoleService
    {
        public Task<BaseResponseModel> GetRolesById(int userId);
        public Task<BaseResponseModel> GetRoleListByGroupId(int userId, int roleGroupID);
    }
}
