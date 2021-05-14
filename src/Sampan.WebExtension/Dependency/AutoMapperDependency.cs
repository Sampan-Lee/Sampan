using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Sampan.WebExtension.Dependency
{
    public static class AutoMapperDependency
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var basePath = AppContext.BaseDirectory;
            var servicesFile = Path.Combine(basePath, "Sampan.Application.dll");
            // 获取 Service.dll 程序集服务，并注册
            if (!File.Exists(servicesFile))
            {
                var msg = "service.dll 丢失，因为项目解 耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                throw new Exception(msg);
            }

            var assemblysServices = Assembly.LoadFrom(servicesFile);
            services.AddAutoMapper(assemblysServices);
        }
    }
}