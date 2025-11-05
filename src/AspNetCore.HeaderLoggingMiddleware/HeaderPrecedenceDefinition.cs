using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.HeaderLoggingMiddleware
{
    public class HeaderPrecedenceDefinition
    {
        public string ScopeOutputKey { get; }
        public List<string> HeaderPrecedenceOrder { get; }

        public HeaderPrecedenceDefinition(string scopeOutputKey, IEnumerable<string> headerPrecedenceOrder)
        {
            if (string.IsNullOrWhiteSpace(scopeOutputKey))
                throw new ArgumentException("Scope output key cannot be null or empty", nameof(scopeOutputKey));
            
            ScopeOutputKey = scopeOutputKey;
            HeaderPrecedenceOrder = headerPrecedenceOrder?.Select(x => x.ToLower()).ToList() 
                ?? throw new ArgumentNullException(nameof(headerPrecedenceOrder));

            if (!HeaderPrecedenceOrder.Any())
                throw new ArgumentException("Header precedence order must contain at least one header", nameof(headerPrecedenceOrder));
        }
    }
}

