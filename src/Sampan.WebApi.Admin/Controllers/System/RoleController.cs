using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampan.Public.Dto;
using Sampan.Service.Contract.System.Roles;
using Sampan.Service.Contract.System;
using Sampan.WebExtension.Model;

namespace Sampan.WebApi.Admin.Controllers.System
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(SystemPermission.Role.Default)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        // GET
        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<RoleDto>> GetAsync([FromRoute] int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpGet]
        public async Task<JsonResultModel<PageResultDto<RoleListDto>>> GetAsync([FromQuery] GetRoleListDto input)
        {
            var result = await _service.GetAsync(input);
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("dictionary")]
        public async Task<JsonResultModel<List<DropdownDto>>> GetDictionaryAsync()
        {
            var result = await _service.GetDictionaryAsync();
            return result.ToSuccess();
        }


        [HttpGet]
        [Route("{id}/permission")]
        public async Task<JsonResultModel<List<int>>> GetPermissionAsync([FromRoute] int id)
        {
            var result = await _service.GetPermissionAsync(id);
            return result.ToSuccess();
        }

        [HttpPost]
        [Route("permission:assign")]
        [Authorize(SystemPermission.Role.AssignPermission)]
        public async Task<JsonResultModel<bool>> AssignPermissionAsync([FromBody] AssignPermissionDto input)
        {
            await _service.AssignPermissionAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        [Authorize(SystemPermission.Role.Create)]
        public async Task<JsonResultModel<object>> CreateAsync([FromBody] CreateRoleDto input)
        {
            await _service.CreateAsync(input);
            return new JsonResultModel<object>();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(SystemPermission.Role.Edit)]
        public async Task<JsonResultModel<object>> UpdateAsync([FromRoute] int id, [FromBody] UpdateRoleDto input)
        {
            await _service.UpdateAsync(id, input);
            return new JsonResultModel<object>();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(SystemPermission.Role.Delete)]
        public async Task<JsonResultModel<object>> DeleteAsync([FromRoute] int id)
        {
            await _service.DeleteAsync(id);
            return new JsonResultModel<object>();
        }
    }
}