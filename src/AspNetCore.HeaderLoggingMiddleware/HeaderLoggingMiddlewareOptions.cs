using System.Collections.Generic;

namespace AspNetCore.HeaderLoggingMiddleware
{
    public class HeaderLoggingMiddlewareOptions : IHeaderLoggingMiddlewareOptions
    {
        public HashSet<string> IncludeHeaders { get; }

        public HeaderLoggingMiddlewareOptions()
        {
            IncludeHeaders = new HashSet<string>();
        }

        private void AddIncludeHeader(string header)
        {
            IncludeHeaders.Add(header.ToLower());
        }

        public HeaderLoggingMiddlewareOptions IncludeBasicHeaders()
        {
            AddIncludeHeader("Accept-Language");
            AddIncludeHeader("Host");
            AddIncludeHeader("Referer");
            AddIncludeHeader("User-Agent");
            return this;
        }

        public HeaderLoggingMiddlewareOptions IncludeAzureHeaders()
        {
            AddIncludeHeader("X-ARR-LOG-ID");
            AddIncludeHeader("X-SITE-DEPLOYMENT-ID");
            AddIncludeHeader("X-WAWS-Unencoded-URL");
            AddIncludeHeader("X-Original-URL");
            return this;
        }

        public HeaderLoggingMiddlewareOptions IncludeCloudflareHeaders()
        {
            AddIncludeHeader("CF-IPCountry");
            AddIncludeHeader("CF-Connecting-IP");
            AddIncludeHeader("CF-RAY");
            return this;
        }

        public HeaderLoggingMiddlewareOptions IncludeXForwardHeaders()
        {
            AddIncludeHeader("X-Forwarded-For");
            AddIncludeHeader("X-Forwarded-For-Proto");
            return this;
        }

        public HeaderLoggingMiddlewareOptions IncludeExtraHeaders(IEnumerable<string> extraHeaders)
        {
            foreach (var extraHeader in extraHeaders)
            {
                AddIncludeHeader(extraHeader);
            }

            return this;
        }
    }
}
