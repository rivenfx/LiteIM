using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    public interface IImClient
    {
        /// <summary>
        /// 向指定的多个客户端id发送消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="receiveClientId">接收者的客户端id</param>
        /// <param name="message">消息</param>
        /// <param name="receipt">是否回执</param>
        void SendMessage(string senderClientId, IEnumerable<string> receiveClientId, object message, bool receipt = false);

        /// <summary>
        /// 获取所在线客户端id
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetClientListByOnline();

        /// <summary>
        /// 判断客户端是否在线
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        bool HasOnline(string clientId);


        #region 群聊频道

        /// <summary>
        /// 加入群聊频道，每次上线都必须重新加入
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chan">群聊频道名</param>
        void JoinChan(string clientId, string chan);

        /// <summary>
        /// 离开群聊频道
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chans">群聊频道名</param>
        void LeaveChan(string clientId, params string[] chans);

        /// <summary>
        /// 获取群聊频道所有客户端id（测试）
        /// </summary>
        /// <param name="chan">群聊频道名</param>
        /// <returns></returns>
        IEnumerable<string> GetChanClientList(string chan);


        /// <summary>
        /// 清理群聊频道的离线客户端（测试）
        /// </summary>
        /// <param name="chan">群聊频道名</param>
        void ClearChanClient(string chan);


        /// <summary>
        /// 获取所有群聊频道和在线人数
        /// </summary>
        /// <returns>频道名和在线人数</returns>
        IEnumerable<(string chan, long online)> GetChanList();

        /// <summary>
        /// 获取用户参与的所有群聊频道
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        IEnumerable<string> GetChanListByClientId(string clientId);


        /// <summary>
        /// 获取群聊频道的在线人数
        /// </summary>
        /// <param name="chan">群聊频道名</param>
        /// <returns>在线人数</returns>
        long GetChanOnline(string chan);

        /// <summary>
        /// 系统广播发送群聊消息，所有在线的用户将收到消息
        /// </summary>
        /// <param name="chan">群聊频道名</param>
        /// <param name="message">消息</param>
        void SendChanMessage(string chan, object message);

        #endregion
    }
}
