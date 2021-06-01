using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM.Extensions
{
    public static class ImKeysExtensions
    {
        public static string Chan(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{ImConsts.Chan}{end}";
        }

        public static string Client(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{ImConsts.Client}{end}";
        }

        public static string ListChan(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{ImConsts.ListChan}";
        }

        public static string Online(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{ImConsts.Online}";
        }

        public static string Offline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{ImConsts.Offline}";
        }
    }


}
