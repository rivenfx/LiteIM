using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM.Extensions
{
    public static class ImKeysExtensions
    {
        /// <summary>
        /// �����-Ƶ��
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string Chan(this string prefix, string end)
        {
            return $"{prefix}{ImConsts.Chan}_{end}";
        }

        /// <summary>
        /// �����-�ͻ���
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string Client(this string prefix, string end)
        {
            return $"{prefix}{ImConsts.Client}_{end}";
        }

        /// <summary>
        /// �����-Ƶ���б�
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ListChan(this string prefix)
        {
            return $"{prefix}{ImConsts.ListChan}";
        }

        /// <summary>
        /// �����-���߿ͻ���
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string Online(this string prefix)
        {
            return $"{prefix}{ImConsts.Online}";
        }

        /// <summary>
        /// �����-���߿ͻ���
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string Offline(this string prefix)
        {
            return $"{prefix}{ImConsts.Offline}";
        }
    }


}
