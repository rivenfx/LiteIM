using CSRedis;

namespace LiteIM
{
    public class CsRedisCoreImClientOptions : ImClientOptions, ICsRedisCoreImClientOptions
    {
        public CSRedisClient Redis { get; set; }
    }
}
