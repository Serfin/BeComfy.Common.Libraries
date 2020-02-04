using System;

namespace BeComfy.Common.Consul
{
    public class ConsulServiceId : IConsulServiceId
    {
        public string ServiceId 
            => Guid.NewGuid().ToString().Substring(0, 7);
    }
}