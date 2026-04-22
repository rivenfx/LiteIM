using StackExchange.Redis;

namespace LiteIM
{
    public interface IStackExchangeRedisImClientOptions : IImClientOptions
    {
        /// <summary>
        /// Redis 连接实例
        /// </summary>
        IConnectionMultiplexer Redis { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        IDatabase Database { get; }
    }
}