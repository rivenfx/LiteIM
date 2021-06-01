using CSRedis;

namespace LiteIM
{
    public class CsRedisCoreImClientOptions : ImClientOptions
    {
        public CSRedisClient Redis { get; set; }
    }
}
