using CorrelationId;
using EmployeeManagement.Entities.AppSettings;
using EmployeeManagement.ExtensionMethod;
using EmployeeManagement.WebApi.Logging.Serilog;
using EmployeeManagement.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.AspNetCore;
using System.IO;

namespace EmployeeManagement.WebApi
{
    public class Startup
    {
        public AppSettings AppSettings = new AppSettings();

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
            Configuration.GetSection("AppSettings").Bind(AppSettings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddCorrelationId();
            services
                .AddMemoryCache()
                .AddSingleton<CorrelationIdEnricher>()
                .AddSingleton<ILoggerFactory>(svc =>
                {

                    var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration) // fetching configuration from appsettings file
                        .Enrich
                        .With(svc.GetService<CorrelationIdEnricher>())
                        .CreateLogger();
                    Log.Logger = logger;

                    return new SerilogLoggerFactory(logger, true);
                });
            services.AddApplicationInsightsTelemetry(Configuration);
            
            services.InjectProperties(AppSettings);
            services.AddLocalization();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("EnableCors");
            app.UseSession();
            app.UseCorrelationId();

            // wired up using extension method into middle ware pipeline.
            app.UserSerilogMiddleware();

            app.UseSwagger();
            if (env.IsDevelopment())
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            app.UseMvc();
        }
    }
}
