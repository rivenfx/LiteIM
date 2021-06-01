using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    public interface IImClientOptions
    {
        /// <summary>
        /// 前缀
        /// </summary>
        string Prefix { get; set; }
        /// <summary>
        /// 负载的服务端
        /// </summary>
        string[] Servers { get; set; }
    }

    public class ImClientOptions : IImClientOptions
    {
        public string Prefix { get; set; } = string.Empty;
        public string[] Servers { get; set; }
    }
}
