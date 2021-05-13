using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sampan.WebExtension.Dependency;
using Sampan.WebExtension.Middleware;
using Sampan.WebExtension.Middleware.Pipeline;

namespace Sampan.WebApi.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreMvc();
            services.AddDistributedCache();
            NlogDependency.AddNloglayout();
            services.AddFreeSql();
            services.AddMapper();
            services.AddPolicyAuthorization();
            services.AddJWTAuthentication();
            services.AddSwagger();
            services.AddCorsSetup();
            services.AddSms();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacDependency>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sampan.WebApi.Admin v1"));
            }

            app.UseCors();

            app.UseNlog();

            app.UseHttpLog();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(); });
        }
    }
}