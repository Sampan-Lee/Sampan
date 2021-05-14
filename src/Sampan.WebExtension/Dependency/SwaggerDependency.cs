using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Sampan.WebExtension.Dependency
{
    public static class SwaggerDependency
    {
        /// <summary>
        /// 注入 Swagger 服务到容器内
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            //var basePath = AppContext.BaseDirectory;
            //var ApiName = Appsettings.app(new string[] {"Startup", "ApiName"});

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Sampan.WebApi.Admin", Version = "v1"});

                // try
                // {
                //     // //默认的第二个参数是false，这个是controller的注释，记得修改兼容Linux 使用小写
                //     options.IncludeXmlComments(
                //         $"{Path.Combine(basePath, $"{Appsettings.app("Startup", "ApiName")?.ToLower()}.xml")}", true);
                //
                //     //这个就是Model层的xml文件名
                //     options.IncludeXmlComments(Path.Combine(basePath, "zqcy.appointment.model.xml"));
                //
                //     //这个就是Entity层的xml文件名
                //     options.IncludeXmlComments(Path.Combine(basePath, "zqcy.appointment.entity.xml"));
                // }
                // catch (Exception ex)
                // {
                //     LogHelper.Error(ex, "ZQCY.Appointment.Model.xml 和 ZQCY.Appointment.Entity.xml 丢失，请检查并拷贝");
                // }

                // options.OperationFilter<SecurityRequirementsOperationFilter>();
                //
                // //文档添加枚举描述
                // //options.DocumentFilter<SwaggerEnumFilter>();
                //
                // options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                // {
                //     Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}(注意两者之间是一个【空格】)",
                //     Name = "Authorization", //jwt默认的参数名称
                //     In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer",
                // });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}(注意两者之间是一个【空格】)",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                };
                options.AddSecurityDefinition("Bearer", securitySchema);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// V1 版本
            /// </summary>
            V1 = 1,
        }
    }
}