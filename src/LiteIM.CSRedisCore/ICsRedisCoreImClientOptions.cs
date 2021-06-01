using CSRedis;

namespace LiteIM
{
    public interface ICsRedisCoreImClientOptions : IImClientOptions
    {
        /// <summary>
        /// Redis实例
        /// </summary>
        CSRedisClient Redis { get; set; }
    }
}
