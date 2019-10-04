using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.HeaderLoggingMiddleware
{
    public class HeaderLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<HeaderLoggingMiddleware> _logger;
        private readonly IHeaderLoggingMiddlewareOptions _headerLoggingMiddlewareOptions;

        public HeaderLoggingMiddleware(
            ILogger<HeaderLoggingMiddleware> logger,
            IHeaderLoggingMiddlewareOptions headerLoggingMiddlewareOptions)
        {
            _logger = logger;
            _headerLoggingMiddlewareOptions = headerLoggingMiddlewareOptions;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var headers = context.Request.Headers.ToDictionary(
                k => k.Key.ToLower(),
                k => k.Value);

            var scope = headers.Where(x => _headerLoggingMiddlewareOptions.IncludeHeaders.Contains(x.Key))
                .ToDictionary<KeyValuePair<string, StringValues>, string, object>(
                    kp => kp.Key,
                    kp => string.Join(", ", kp.Value));

            using (_logger.BeginScope(scope))
            {
                await next(context);
            }
        }
    }
}
