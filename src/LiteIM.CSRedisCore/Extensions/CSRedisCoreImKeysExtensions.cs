using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM.Extensions
{
    public static class CSRedisCoreImKeysExtensions
    {
        public static string CSRedisCoreChan(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Chan(end)}";
        }

        public static string CSRedisCoreClient(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Client(end)}";
        }

        public static string CSRedisCoreListChan(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.ListChan()}";
        }

        public static string CSRedisCoreOnline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Online()}";
        }

        public static string CSRedisCoreOffline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Offline()}";
        }
    }


}
