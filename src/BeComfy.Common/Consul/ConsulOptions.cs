namespace BeComfy.Common.Consul
{
    public class ConsulOptions
    {
        public string ConsulAgentAddress { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public int ServicePort { get; set; }
        public bool HealthcheckEnabled { get; set; }
        public string PingEndpoint { get; set; }
        public int PingInterval { get; set; }
        public int DeregisterAfter { get; set; }
    }
}