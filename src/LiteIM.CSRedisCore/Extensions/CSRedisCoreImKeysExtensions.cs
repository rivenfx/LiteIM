using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM.Extensions
{
    public static class CSRedisCoreImKeysExtensions
    {
        /// <summary>
        /// 缓存键-频道
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string CSRedisCoreChan(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Chan(end)}";
        }

        /// <summary>
        /// 缓存键-客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string CSRedisCoreClient(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Client(end)}";
        }

        /// <summary>
        /// 缓存键-频道列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string CSRedisCoreListChan(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.ListChan()}";
        }

        /// <summary>
        /// 缓存键-在线客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string CSRedisCoreOnline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Online()}";
        }

        /// <summary>
        /// 缓存键-离线客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string CSRedisCoreOffline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Offline()}";
        }
    }


}
