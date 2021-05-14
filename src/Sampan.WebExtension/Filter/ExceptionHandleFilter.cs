using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sampan.Common.Util;
using Sampan.WebExtension.Model;

namespace Sampan.WebExtension.Filter
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionHandleFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionHandleFilter> _logger;

        public ExceptionHandleFilter(IModelMetadataProvider modelMetadataProvider,
            ILogger<ExceptionHandleFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 没有处理的异常，就会进来
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled) //异常有没有被处理过
            {
                string controllerName = (string) filterContext.RouteData.Values["controller"];
                string actionName = (string) filterContext.RouteData.Values["action"];
                string errorMsg = filterContext.Exception.Message;
                string msgTemplate = $"在执行 controller[{controllerName}] 的 action[{actionName}] 时产生异常,异常信息:{errorMsg}";
                bool isWriteLog = true;
                int statusCode = HttpStatusCode.BadRequest;

                #region 日志分类统一处理

                if (filterContext.Exception is BusinessException)
                {
                    //业务异常给出提示语  不记录日志
                    isWriteLog = false;
                    statusCode = HttpStatusCode.BusinessError;
                }
                else if (filterContext.Exception is ArgumentException)
                {
                    errorMsg = ResultMessage.ServerConnectionError;
                    statusCode = HttpStatusCode.ArgumentError;
                }
                else if (filterContext.Exception is Exception)
                {
                    errorMsg = ResultMessage.ServerConnectionError;
                }
                else
                {
                    errorMsg = ResultMessage.ServerConnectionError;
                    statusCode = HttpStatusCode.BadRequest;
                }

                #endregion

                //区分异常是否记录日志
                if (isWriteLog) _logger.LogError(filterContext.Exception, msgTemplate);

                //不是正式环境提示语还是抛出异常信息
                if (!EnvUtil.IsOnline) errorMsg = filterContext.Exception.Message;

                //处理 http请求的状态码
                //filterContext.HttpContext.Response.StatusCode = statusCode;

                //api 不区分处理是不是 ajax请求
                filterContext.Result = new JsonResult(
                    new JsonResultModel<object>
                    {
                        status = false,
                        code = statusCode,
                        errorMsg = errorMsg,
                    });
                filterContext.ExceptionHandled = true;
            }
        }
    }
}