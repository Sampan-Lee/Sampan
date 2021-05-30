using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampan.Public.Dto;
using Sampan.Service.Contract.System.Logs;
using Sampan.WebExtension.Model;

namespace Sampan.WebApi.Admin.Controllers.System
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly ILogService _service;

        // GET
        public LogController(ILogService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResultModel<LogDto>> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);
            return result.ToSuccess();
        }

        [HttpGet]
        public async Task<JsonResultModel<PageResultDto<LogListDto>>> GetAsync([FromQuery] GetLogListDto input)
        {
            var result = await _service.GetAsync(input);
            return result.ToSuccess();
        }
    }
}