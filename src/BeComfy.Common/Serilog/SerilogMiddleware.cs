using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BeComfy.Common.Serilog
{
    public class SerilogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public SerilogMiddleware(RequestDelegate next, ILogger<SerilogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(" -> Middleware working <- ");
            await _next(context);
        }
    }
}