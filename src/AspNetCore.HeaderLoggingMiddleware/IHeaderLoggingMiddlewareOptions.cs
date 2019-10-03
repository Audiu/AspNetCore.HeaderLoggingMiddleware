using System.Collections.Generic;

namespace AspNetCore.HeaderLoggingMiddleware
{
    public interface IHeaderLoggingMiddlewareOptions
    {
        HashSet<string> IncludeHeaders { get; }
    }
}