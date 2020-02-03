using System.Collections.Generic;

namespace BeComfy.Common.RestEase
{
    public class RestEaseOptions
    {
        public IEnumerable<RestEaseService> Services { get; set; }
        
        public class RestEaseService
        {
            public string Name { get; set; }
            public string Scheme { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        }
    }
}