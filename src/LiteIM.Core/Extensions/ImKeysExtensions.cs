using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM.Extensions
{
    public static class ImKeysExtensions
    {
        /// <summary>
        /// 缓存键-频道
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string Chan(this string prefix, string end)
        {
            return $"{prefix}{ImConsts.Chan}_{end}";
        }

        /// <summary>
        /// 缓存键-客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string Client(this string prefix, string end)
        {
            return $"{prefix}{ImConsts.Client}_{end}";
        }

        /// <summary>
        /// 缓存键-频道列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ListChan(this string prefix)
        {
            return $"{prefix}{ImConsts.ListChan}";
        }

        /// <summary>
        /// 缓存键-在线客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string Online(this string prefix)
        {
            return $"{prefix}{ImConsts.Online}";
        }

        /// <summary>
        /// 缓存键-离线客户端
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string Offline(this string prefix)
        {
            return $"{prefix}{ImConsts.Offline}";
        }
    }


}
