using System;
using System.Diagnostics;
using System.Threading;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Sampan.Common.Log;
using Sampan.Common.Util;
using Sampan.Public.Entity;

namespace Sampan.WebExtension.Dependency
{
    public static class FreeSqlDependency
    {
        public static void AddFreeSql(this IServiceCollection services)
        {
            IFreeSql fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql,
                    DesEncryptUtil.Decrypt(Appsettings.app("ConnectionStrings", "MySqlConnectionString")))
                .UseNoneCommandParameter(true)
                .UseMonitorCommand(cmd => { Trace.WriteLine(cmd.CommandText + ";"); })
                .Build();

            fsql.Aop.CurdAfter += (s, e) =>
            {
                Console.WriteLine(e.Sql);
                LogHelper.Info(e.Sql);

                if (e.ElapsedMilliseconds > 200)
                {
                    LogHelper.Warning("Sql执行超时，请注意查看");
                }
            };

            services.AddSingleton(fsql);
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository(filter => filter.Apply<ISoftDeleteEntity>("IsDelete", a => a.IsDelete == false));
        }
    }
}