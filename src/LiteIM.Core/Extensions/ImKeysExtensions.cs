using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM.Extensions
{
    public static class ImKeysExtensions
    {
        public static string Chan(this string prefix, string end)
        {
            return $"{prefix}{ImConsts.Chan}_{end}";
        }

        public static string Client(this string prefix, string end)
        {
            return $"{prefix}{ImConsts.Client}_{end}";
        }

        public static string ListChan(this string prefix)
        {
            return $"{prefix}{ImConsts.ListChan}";
        }

        public static string Online(this string prefix)
        {
            return $"{prefix}{ImConsts.Online}";
        }

        public static string Offline(this string prefix)
        {
            return $"{prefix}{ImConsts.Offline}";
        }
    }


}
