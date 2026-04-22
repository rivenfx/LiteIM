namespace LiteIM.Extensions
{
    public static class StackExchangeRedisImKeysExtensions
    {
        /// <summary>
        /// 前缀-频道
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string StackExchangeRedisChan(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Chan(end)}";
        }

        /// <summary>
        /// 前缀-客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string StackExchangeRedisClient(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Client(end)}";
        }

        /// <summary>
        /// 前缀-频道列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string StackExchangeRedisListChan(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.ListChan()}";
        }

        /// <summary>
        /// 前缀-在线客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string StackExchangeRedisOnline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Online()}";
        }

        /// <summary>
        /// 前缀-离线客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string StackExchangeRedisOffline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Offline()}";
        }
    }
}