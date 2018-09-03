using Microsoft.AspNetCore.Builder;

namespace EmployeeManagement.WebApi.Middleware
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SerilogMiddlewareExtension
    {
        public static IApplicationBuilder UserSerilogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SerilogMiddleware>();
        }
    }
}
