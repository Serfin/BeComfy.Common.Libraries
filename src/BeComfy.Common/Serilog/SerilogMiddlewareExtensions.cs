using Microsoft.AspNetCore.Builder;

namespace BeComfy.Common.Serilog
{
    public static class SerilogMiddlewareExtensions
    {
        public static IApplicationBuilder UseSerilog(this IApplicationBuilder builder)
            => builder.UseMiddleware<SerilogMiddleware>();
    }
}