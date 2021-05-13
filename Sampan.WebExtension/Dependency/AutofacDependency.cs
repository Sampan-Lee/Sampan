using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Sampan.Infrastructure.AOP.Cacheable;
using Sampan.Infrastructure.AOP.Transactional;
using Sampan.Infrastructure.CurrentUser;
using Sampan.Infrastructure.Repository;
using Sampan.Tasks;

namespace Sampan.WebExtension.Dependency
{
    public class AutofacDependency : Autofac.Module
    {
        /// <summary>
        /// 注册容器
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            builder.RegisterType<CurrentUser>().As<ICurrentUser>()
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();

            builder.RegisterGeneric(typeof(FreeSqlRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<MigrationStartupTask>().SingleInstance();
            builder.RegisterBuildCallback(async (c) => await c.Resolve<MigrationStartupTask>().StartAsync());

            var servicesFile = Path.Combine(basePath, "Sampan.Application.dll");
            // 获取 Service.dll 程序集服务，并注册
            if (!File.Exists(servicesFile))
            {
                var msg = "Service.dll 丢失，因为项目解 耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                throw new Exception(msg);
            }

            var types = new[]
            {
                typeof(CacheableInterceptor),
                typeof(TransactionalInterceptor),
            };
            builder.RegisterTypes(types);
            builder.RegisterType(typeof(TransactionalAsyncInterceptor));

            var assemblysServices = Assembly.LoadFrom(servicesFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired()
                .EnableInterfaceInterceptors()
                .InterceptedBy(types);

            var domainFile = Path.Combine(basePath, "Sampan.Domain.dll");
            // 获取 Service.dll 程序集服务，并注册
            if (!File.Exists(domainFile))
            {
                var msg = "Domain.dll 丢失，因为项目解 耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                throw new Exception(msg);
            }

            var assemblysDomain = Assembly.LoadFrom(domainFile);
            builder.RegisterAssemblyTypes(assemblysDomain)
                .PropertiesAutowired()
                .InstancePerDependency();


            var infrastructureFile = Path.Combine(basePath, "Sampan.Infrastructure.dll");
            // 获取 Service.dll 程序集服务，并注册
            if (!File.Exists(infrastructureFile))
            {
                var msg = "Infrastructure.dll 丢失，因为项目解 耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                throw new Exception(msg);
            }

            var assemblysInfrastructure = Assembly.LoadFrom(infrastructureFile);
            builder.RegisterAssemblyTypes(assemblysInfrastructure)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired()
                .EnableInterfaceInterceptors();
        }
    }
}