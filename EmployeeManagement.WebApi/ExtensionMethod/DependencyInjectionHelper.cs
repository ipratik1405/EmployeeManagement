using EmployeeManagement.Business;
using EmployeeManagement.BusinessLayer;
using EmployeeManagement.Entities.AppSettings;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace EmployeeManagement.ExtensionMethod
{
    public static class DependencyInjectionHelper
    {
        public static void InjectProperties(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton(appSettings);
            services.AddSingleton(typeof(IServices<>), typeof(Services<>));

            var policy = new CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;
            services.AddCors(x => x.AddPolicy("EnableCors", policy)).BuildServiceProvider();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }
    }
}
