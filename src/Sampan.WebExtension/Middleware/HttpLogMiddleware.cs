using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Sampan.Common.Log;
using Sampan.Common.Util;

namespace Sampan.WebExtension.Middleware
{
    /// <summary>
    /// 中间件
    /// 记录请求和响应数据
    /// </summary>
    public class HttpLogMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public HttpLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //记录上线文请求的唯一表示 这个中间件比较靠前目前记录在这里
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            //初始化时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            #region 执行逻辑

            if (Appsettings.app("Middleware", "RequestResponseLog", "Enabled").ToBool())
            {
                context.Request.EnableBuffering();
                Stream originalBody = context.Response.Body;

                try
                {
                    // 存储请求数据
                    await RequestDataLog(context);

                    using (var ms = new MemoryStream())
                    {
                        context.Response.Body = ms;

                        await _next(context);

                        // 存储响应数据
                        //ResponseDataLog(context.Response, ms);
                        ms.Position = 0;
                        await ms.CopyToAsync(originalBody);

                        stopwatch.Stop();
                        LogHelper.Info($"执行耗时:{stopwatch.ElapsedMilliseconds}ms");
                    }
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    // 记录异常
                    //ErrorLogData(context.Response, ex);
                }
                finally
                {
                    context.Response.Body = originalBody;
                }
            }
            else
            {
                await _next(context);
                stopwatch.Stop();
            }

            #endregion
        }

        private async Task RequestDataLog(HttpContext context)
        {
            var request = context.Request;
            var sr = new StreamReader(request.Body);

            var content = $" QueryData:{request.Path + request.QueryString}\r\n BodyData:{await sr.ReadToEndAsync()}";

            if (!string.IsNullOrEmpty(content))
            {
                Parallel.For(0, 1, e =>
                {
                    LogHelper.Info(String.Join("\r\n",
                        new string[] {"Request Data:", content}));
                });

                request.Body.Position = 0;
            }
        }

        ///返回的数据暂时关闭掉不显示
        //private void ResponseDataLog(HttpResponse response, MemoryStream ms)
        //{
        //    ms.Position = 0;
        //    var ResponseBody = new StreamReader(ms).ReadToEnd();

        //    // 去除 Html
        //    var reg = "<[^>]+>";
        //    var isHtml = Regex.IsMatch(ResponseBody, reg);

        //    if (!string.IsNullOrEmpty(ResponseBody))
        //    {
        //        Parallel.For(0, 1, e =>
        //        {
        //            //LogLock.OutSql2Log("RequestResponseLog", new string[] { "Response Data:", ResponseBody });

        //            //LogManagerNlog.LogInformation(String.Join("\r\n", new string[] { "RequestResponseLog_Response_Data:", ResponseBody }));
        //        });

        //    }
        //}
    }
}