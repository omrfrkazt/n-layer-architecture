using Api.Controllers.BaseController;
using Api.Filter;
using Business.Helper;
using Business.Services.User;
using Dto.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class UserController : BaseApi
    {
        #region Fields&Ctor
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Method
        [HasPermission(Permissions.All)]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            return Ok(response);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserModel model)
        {
            var response = await _userService.AddAsync(model);
            return Ok(response);
        }
        #endregion

    }
}
