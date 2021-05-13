using System;
using Microsoft.Extensions.DependencyInjection;
using Sampan.Common.Extension;
using Sampan.Common.Util;
using Sampan.Infrastructure.TaskScheduler;

namespace Sampan.WebExtension.Dependency
{
    public static class TaskDependency
    {
        public static void AddTaskSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //ITaskJobService 构造需要 SchedulerCenter 在这里提前注入
            services.AddSingleton<ISchedulerCenter>();

            //任务是否需要开启
            if (!Appsettings.app("Middleware", "Job", "Enabled").ObjToBool()) return;

            //services.AddTransient<AppointmentResourceJob>();
            //services.AddTransient<AppointmentOrderTimeOutJob>();
        }
    }
}