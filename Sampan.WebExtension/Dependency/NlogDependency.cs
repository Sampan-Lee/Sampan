using System;
using System.Collections.Generic;
using NLog.Config;
using Sampan.Common.Log;

namespace Sampan.WebExtension.Dependency
{
    public static class NlogDependency
    {
        private static Dictionary<string, Type> _layoutRenderers => new Dictionary<string, Type>()
        {
            {"traceId", typeof(TraceIdLayoutRenderer)},
            {"clientip", typeof(ClientIPLayoutRenderer)},
            {"requesturl", typeof(RequestUrlLayoutRenderer)},
            {"userid", typeof(UserIdLayoutRenderer)},
        };

        /// <summary>
        /// 注册模版页
        /// </summary>
        public static void AddNloglayout()
        {
            foreach (var item in _layoutRenderers)
            {
                ConfigurationItemFactory
                    .Default
                    .LayoutRenderers
                    .RegisterDefinition(item.Key, item.Value);
            }
        }
    }
}