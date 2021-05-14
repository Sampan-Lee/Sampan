using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Authorize(SystemPermission.Menu.Create)]
        public async Task<JsonResultModel<bool>> CreateMenuAsync([FromBody] CreateMenuDto input)
        {
            await _service.CreateAsync(input);
            return true.ToSuccess();
        }
    }
}