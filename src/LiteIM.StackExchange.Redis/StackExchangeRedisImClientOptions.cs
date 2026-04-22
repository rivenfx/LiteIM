using StackExchange.Redis;

namespace LiteIM
{
    public class StackExchangeRedisImClientOptions : ImClientOptions, IStackExchangeRedisImClientOptions
    {
        public IConnectionMultiplexer Redis { get; set; }
    }
}