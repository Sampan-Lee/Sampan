using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using System.Diagnostics;
using System.Text;
using Sampan.Common.Util;

namespace Sampan.Common.Log
{
    /// <summary>
    /// 链路标识
    /// </summary>
    [LayoutRenderer("traceId")]
    [ThreadAgnostic]
    public class TraceIdLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            // 记录请求上下文的标识符 数据库标识ActivityId
            builder.Append(Trace.CorrelationManager.ActivityId.ToString());
        }
    }

    [LayoutRenderer("clientip")]
    [ThreadAgnostic]
    public class ClientIPLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            // 记录请求真实Ip
            builder.Append(base.HttpContextAccessor.HttpContext.GetClientIP());
        }
    }

    [LayoutRenderer("requesturl")]
    [ThreadAgnostic]
    public class RequestUrlLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            //请求路径
            builder.Append(base.HttpContextAccessor.HttpContext.Request.Path);
        }
    }

    [LayoutRenderer("userid")]
    [ThreadAgnostic]
    public class UserIdLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var value = HttpContextAccessor.HttpContext.GetUserId();
            if (value.HasValue) builder.Append(value.Value);
        }
    }
}