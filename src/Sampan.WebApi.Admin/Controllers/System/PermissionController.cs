using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampan.Service.Contract.System.Permissions;
using Sampan.Service.Contract.System.Permissions.Dtos;
using Sampan.WebExtension.Model;

namespace Sampan.WebApi.Admin.Controllers.System
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResultModel<List<PermissionModuleDto>>> GetAsync()
        {
            var result = await _service.GetAsync();
            return result.ToSuccess();
        }

        [HttpGet]
        [Route("menu-permission")]
        public async Task<JsonResultModel<List<MenuPermissionDto>>> GetMenuPermissionAsync()
        {
            var result = await _service.GetMenuPermissionAsync();
            return result.ToSuccess();
        }
    }
}