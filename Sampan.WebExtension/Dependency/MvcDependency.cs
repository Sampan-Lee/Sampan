using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sampan.Common.Constant;
using Sampan.Common.Util;
using Sampan.WebExtension.Filter;

namespace Sampan.WebExtension.Dependency
{
    public static class MvcDependency
    {
        public static void AddCoreMvc(this IServiceCollection services)
        {
            services.AddSingleton(new Appsettings());

            services.AddControllers(options =>
                {
                    options.Filters.Add<ExceptionHandleFilter>(); //全局注册
                    options.Filters.Add<CustomResultFilter>(); //参数校验
                    options.Filters.Add<DuplicateSubmissionActionFilter>(); //重复提交
                })
                .AddNewtonsoftJson(options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key 目前是开启的
                    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = DateTimeFormatConst.yyyy_MM_ddHHmmss;
                });

            //hcc文件上传默认大小不受限制 后续加上大小限制 2020-6-22
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
        }
    }
}