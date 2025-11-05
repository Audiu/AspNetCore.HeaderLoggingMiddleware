using System;
using System.Collections.Generic;

namespace AspNetCore.HeaderLoggingMiddleware
{
    public class HeaderLoggingMiddlewareOptions : IHeaderLoggingMiddlewareOptions
    {
        public HashSet<string> IncludeHeaders { get; }
        
        public List<HeaderPrecedenceDefinition> HeaderPrecedenceDefinitions { get; private set; }

        public HeaderLoggingMiddlewareOptions()
        {
            IncludeHeaders = new HashSet<string>();
            HeaderPrecedenceDefinitions = new List<HeaderPrecedenceDefinition>();
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
        
        public HeaderLoggingMiddlewareOptions AddHeaderPrecedence(string scopeOutputKey, IEnumerable<string> headerPrecedenceOrder)
        {
            var definition = new HeaderPrecedenceDefinition(scopeOutputKey, headerPrecedenceOrder);
            HeaderPrecedenceDefinitions.Add(definition);
            
            return this;
        }
        
        [Obsolete("Use AddHeaderPrecedence instead. This method will be removed in a future version.")]
        public HeaderLoggingMiddlewareOptions UseIpHeaderDetection(string scopeOutputKey, List<string> ipHeaderPrecedenceOrder)
        {
            return AddHeaderPrecedence(scopeOutputKey, ipHeaderPrecedenceOrder);
        }
    }
}
