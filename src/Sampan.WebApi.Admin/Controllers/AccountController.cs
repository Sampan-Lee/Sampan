using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampan.Common.Extension;
using Sampan.Infrastructure.CurrentUser;
using Sampan.Service.Contract.Account.AdminAccounts;
using Sampan.Service.Contract.System.SystemUsers;
using Sampan.WebExtension.Authentication;
using Sampan.WebExtension.Model;

namespace Sampan.WebApi.Admin.Controllers.System
{
    [ApiController]
    [Route("api/admin")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAdminAccountService _service;

        public AccountController(IAdminAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("captcha")]
        [AllowAnonymous]
        public async Task<JsonResultModel<bool>> GetCaptchaAsync([FromQuery] string phone)
        {
            var result = await _service.SendCaptchaAsync(phone);
            return result.ToSuccess();
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<JsonResultModel<String>> CreateTokenAsync([FromBody] LoginAdminDto input)
        {
            var loginUser = await _service.LoginAsync(input);
            string token = string.Empty;
            if (loginUser is {Id: > 0})
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Sid, loginUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, loginUser.Name));
                claims.Add(new Claim(SampanClaimTypes.IsAdmin, true.ToString()));
                claims.Add(new Claim(SampanClaimTypes.IsSuperAdmin, (loginUser.Id == 1).ToString()));
                //查询出用户对应的权限角色  
                token = JWTService.GetToken(claims);
            }

            return new JsonResultModel<string>
            {
                status = true,
                code = token.IsNullOrWhiteSpace() ? HttpStatusCode.LoginFail : HttpStatusCode.OK,
                data = token
            };
        }

        [HttpGet]
        [Route("info")]
        [AllowAnonymous]
        public async Task<JsonResultModel<AdminUserDto>> GetAsync(string token)
        {
            var userDto = await _service.GetUserAsync();
            return userDto.ToSuccess();
        }

        [HttpGet]
        [Route("menu")]
        public async Task<JsonResultModel<List<AdminMenuDto>>> GetMenuAsync()
        {
            var result = await _service.GetMenuAsync();
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("permission")]
        public async Task<JsonResultModel<List<string>>> GetPermissionAsync()
        {
            var result = await _service.GetPermissionAsync();
            return result.ToSuccess();
        }
    }
}