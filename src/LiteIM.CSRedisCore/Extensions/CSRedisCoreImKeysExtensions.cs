using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM.Extensions
{
    public static class CSRedisCoreImKeysExtensions
    {
        /// <summary>
        /// �����-Ƶ��
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string CSRedisCoreChan(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Chan(end)}";
        }

        /// <summary>
        /// �����-�ͻ���
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string CSRedisCoreClient(this string prefix, string end)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Client(end)}";
        }

        /// <summary>
        /// �����-Ƶ���б�
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string CSRedisCoreListChan(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.ListChan()}";
        }

        /// <summary>
        /// �����-���߿ͻ���
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string CSRedisCoreOnline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Online()}";
        }

        /// <summary>
        /// �����-���߿ͻ���
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string CSRedisCoreOffline(this string prefix)
        {
            return $"n:{prefix}LiteIM,c:{string.Empty.Offline()}";
        }
    }


}
