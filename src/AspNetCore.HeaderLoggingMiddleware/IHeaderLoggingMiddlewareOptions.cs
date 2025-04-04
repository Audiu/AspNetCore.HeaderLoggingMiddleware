using System.Collections.Generic;

namespace AspNetCore.HeaderLoggingMiddleware
{
    public interface IHeaderLoggingMiddlewareOptions
    {
        HashSet<string> IncludeHeaders { get; }
        string ScopeOutputKey { get; }
        List<string> IpHeaderPrecedenceOrder { get; }
    }
}