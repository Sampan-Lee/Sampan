using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampan.Common.Extension;
using Sampan.Infrastructure.CurrentUser;
using Sampan.Service.Contract.Account.UserAccounts;
using Sampan.WebExtension.Authentication;
using Sampan.WebExtension.Model;

namespace Sampan.WebApi.Front.Controllers.Authorize
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountService _service;

        public AccountController(IUserAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("captcha")]
        [AllowAnonymous]
        public async Task<JsonResultModel<bool>> GetCaptchaAsync([FromQuery] string phone)
        {
            var result = await _service.SendCaptcha(phone);
            return result.ToSuccess();
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<JsonResultModel<String>> CreateTokenAsync([FromBody] LoginUserDto input)
        {
            var loginUser = await _service.LoginAsync(input);
            string token = string.Empty;
            if (loginUser is {Id: > 0})
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Sid, loginUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, loginUser.Name));
                claims.Add(new Claim(SampanClaimTypes.IsAdmin, false.ToString()));
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
    }
}