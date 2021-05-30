using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampan.Public.Dto;
using Sampan.Service.Contract.System.Menus;
using Sampan.Service.Contract.System;
using Sampan.WebExtension.Model;

namespace Sampan.WebApi.Admin.Controllers.System
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(SystemPermission.Menu.Default)]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResultModel<List<MenuListDto>>> GetAsync()
        {
            var result = await _service.GetAsync();
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<MenuDto>> GetAsync([FromRoute] int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpPost]
        [Authorize(SystemPermission.Menu.Create)]
        public async Task<JsonResultModel<bool>> CreateAsync([FromBody] CreateMenuDto input)
        {
            await _service.CreateAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("isEnable:set")]
        [Authorize(SystemPermission.Menu.SetIsEnable)]
        public async Task<JsonResultModel<bool>> SetIsEnableAsync([FromBody] SetEnableDto input)
        {
            await _service.SetIsEnableAsync(input);
            return true.ToSuccess();
        }

        [HttpPost]
        [Route("permission:bind")]
        [Authorize(SystemPermission.Menu.BindPermission)]
        public async Task<JsonResultModel<bool>> SetIsEnableAsync([FromBody] BindPermissionDto input)
        {
            await _service.BindPermissionAsync(input);
            return true.ToSuccess();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(SystemPermission.Menu.Edit)]
        public async Task<JsonResultModel<bool>> UpdateAsync([FromRoute] int id, [FromBody] UpdateMenuDto input)
        {
            await _service.UpdateAsync(id, input);
            return true.ToSuccess();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(SystemPermission.Menu.Delete)]
        public async Task<JsonResultModel<bool>> DeleteAsync([FromRoute] int id)
        {
            await _service.DeleteAsync(id);
            return true.ToSuccess();
        }
    }
}