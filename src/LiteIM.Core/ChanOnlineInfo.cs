using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    /// <summary>
    /// 频道在线人数信息
    /// </summary>
    public struct ChanOnlineInfo
    {
        /// <summary>
        /// 频道
        /// </summary>
        public string Chan { get; }

        /// <summary>
        /// 在线人数
        /// </summary>
        public long Online { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="chan">频道</param>
        /// <param name="online">在线人数</param>
        public ChanOnlineInfo(string chan, long online)
        {
            Chan = chan;
            Online = online;
        }
    }
}
