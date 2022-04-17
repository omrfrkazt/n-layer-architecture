using Api.Controllers.BaseController;
using Business.Services;
using Dto.Common;
using Dto.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AuthController : BaseApi
    {
        #region Fields&Ctor
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        #endregion
        #region Methods
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var response = await _authenticationService.LoginAsync(model);
            return Ok(response);
        }

        [HttpPost("Auth1")]
        public async Task<IActionResult> Auth1([FromBody] LoginModel model)
        {
            return Ok();
        }

        [HttpPost("Auth2")]
        public async Task<IActionResult> Auth2([FromBody] LoginModel model)
        {
            return Ok();
        }
        #endregion
    }
}
