using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using Sampan.Common.Util;
using Sampan.Infrastructure.DistributedCache;
using Sampan.WebExtension.Model;

namespace Sampan.WebExtension.Filter
{
    /// <summary>
    /// 重复提交过滤器
    /// </summary>
    public class DuplicateSubmissionActionFilter : ActionFilterAttribute
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<DuplicateSubmissionActionFilter> _logger;

        public DuplicateSubmissionActionFilter(IDistributedCache distributedCache,
            ILogger<DuplicateSubmissionActionFilter> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (context.HttpContext.Request.Method == "POST")
                {
                    foreach (var parameter in context.ActionDescriptor.Parameters)
                    {
                        var parameterName = parameter.Name; //获取Action方法中参数的名字
                        var parameterType = parameter.ParameterType; //获取Action方法中参数的类型

                        var pointRequest = context.ActionArguments[parameterName];

                        var entityHash = HashUtil.GetHash(pointRequest);


                        if (_distributedCache.ExistAsync(entityHash).Result)
                        {
                            context.Result = new JsonResult(
                                new JsonResultModel<object>()
                                {
                                    status = false,
                                    code = HttpStatusCode.BadRequest,
                                    errorMsg = "请勿重复提交表单"
                                });
                        }
                        else
                        {
                            _distributedCache.SetAsync(entityHash, true, TimeSpan.FromSeconds(2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DuplicateSubmissionActionFilter处理异常");
            }

            base.OnActionExecuting(context);
        }
    }
}