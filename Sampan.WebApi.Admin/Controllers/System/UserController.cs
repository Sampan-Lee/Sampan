using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampan.Public.Dto;
using Sampan.Service.Contract.System;
using Sampan.Service.Contract.System.SystemUsers;
using Sampan.WebExtension.Model;

namespace Sampan.WebApi.Admin.Controllers.System
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(SystemPermission.SystemUser.Default)]
    public class UserController : ControllerBase
    {
        private readonly IAdminUserService _service;

        // GET
        public UserController(IAdminUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<AdminUserDto>> GetAsync([FromRoute] int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpGet]
        public async Task<JsonResultModel<PageDto<AdminUserListDto>>> GetAsync([FromQuery] GetAdminUserListDto input)
        {
            var result = await _service.GetAsync(input);
            return result.ToSuccess();
        }

        [HttpPost]
        [Authorize(SystemPermission.SystemUser.Create)]
        public async Task<JsonResultModel<bool>> CreateAsync([FromBody] CreateAdminUserDto input)
        {
            await _service.CreateAsync(input);
            return true.ToSuccess();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(SystemPermission.SystemUser.Edit)]
        public async Task<JsonResultModel<bool>> UpdateAsync([FromRoute] int id, [FromBody] UpdateAdminUserDto input)
        {
            await _service.UpdateAsync(id, input);
            return true.ToSuccess();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(SystemPermission.SystemUser.Delete)]
        public async Task<JsonResultModel<bool>> DeleteAsync([FromRoute] int id)
        {
            await _service.DeleteAsync(id);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("password:reset")]
        [Authorize(SystemPermission.SystemUser.ResetPassword)]
        public async Task<JsonResultModel<bool>> ResetPasswordAsync([FromBody] ResetPasswordDto input)
        {
            await _service.ResetPasswordAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("isEnable:set")]
        [Authorize(SystemPermission.SystemUser.SetIsEnable)]
        public async Task<JsonResultModel<bool>> SetIsEnableAsync([FromBody] SetEnableDto input)
        {
            await _service.SetIsEnableAsync(input);
            return true.ToSuccess();
        }
    }
}