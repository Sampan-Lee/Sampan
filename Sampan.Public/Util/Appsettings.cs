using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Sampan.Common.Util
{
    /// <summary>
    /// appsettings.json操作类
    /// Appsettings.app(new string[] { "Audience", "Secret" });
    /// </summary>
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }

        public Appsettings(string contentPath = null)
        {
            //string Path = "appsettings.Production.json";

            //根据环境变量来取配置文件
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string Path = string.IsNullOrWhiteSpace(env) ? "appsettings.Development.json" : $"appsettings.{env}.json";

            //LogManagerNlog.LogInformation($"项目启动配置文件：{Path} ");
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path)
                .Add(new JsonConfigurationSource
                {
                    Path = Path, Optional = false, ReloadOnChange = true
                }) //这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
                .Build();
        }

        public Appsettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch
            {
                //LogManagerNlog.LogError($"配置文件获取错误:{string.Join(":", sections)}");
            }

            return "";
        }

        /// <summary>
        /// 递归获取配置信息数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static List<T> app<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }
    }
}