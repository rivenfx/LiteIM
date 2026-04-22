using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiteIM
{
    public interface IImClient
    {
        /// <summary>
        /// 获取所在线客户端id。
        /// </summary>
        /// <remarks>同步方法仅用于兼容旧调用方。在 ASP.NET Core、SignalR 等高并发服务端路径中，优先使用 Async 结尾方法以避免阻塞请求线程。</remarks>
        /// <returns></returns>
        IEnumerable<string> GetClientListByOnline();

        /// <summary>
        /// 异步获取所在线客户端id
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetClientListByOnlineAsync();

        /// <summary>
        /// 判断客户端是否在线。
        /// </summary>
        /// <remarks>同步方法仅用于兼容旧调用方。在高并发服务端路径中，优先使用 HasOnlineAsync。</remarks>
        /// <param name="clientId"></param>
        /// <returns></returns>
        bool HasOnline(string clientId);

        /// <summary>
        /// 异步判断客户端是否在线
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<bool> HasOnlineAsync(string clientId);


        #region 频道

        /// <summary>
        /// 加入频道，每次上线都必须重新加入。
        /// </summary>
        /// <remarks>同步方法会阻塞当前线程，建议仅用于兼容旧代码；新的服务端调用优先使用 JoinChanAsync。</remarks>
        /// <param name="clientId">客户端id</param>
        /// <param name="chan">频道名</param>
        void JoinChan(string clientId, string chan);

        /// <summary>
        /// 异步加入频道，每次上线都必须重新加入
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chan">频道名</param>
        Task JoinChanAsync(string clientId, string chan);

        /// <summary>
        /// 离开频道。
        /// </summary>
        /// <remarks>同步方法会阻塞当前线程，建议仅用于兼容旧代码；新的服务端调用优先使用 LeaveChanAsync。</remarks>
        /// <param name="clientId">客户端id</param>
        /// <param name="chans">频道名</param>
        void LeaveChan(string clientId, params string[] chans);

        /// <summary>
        /// 异步离开频道
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chans">频道名</param>
        Task LeaveChanAsync(string clientId, params string[] chans);

        /// <summary>
        /// 离开所有频道。
        /// </summary>
        /// <remarks>同步方法会阻塞当前线程，建议仅用于兼容旧代码；新的服务端调用优先使用 LeaveChanAsync。</remarks>
        /// <param name="clientId">客户端id</param>
        void LeaveChan(string clientId);

        /// <summary>
        /// 异步离开所有频道
        /// </summary>
        /// <param name="clientId">客户端id</param>
        Task LeaveChanAsync(string clientId);

        /// <summary>
        /// 获取频道所有客户端id（测试）。
        /// </summary>
        /// <remarks>同步方法仅用于兼容旧调用方；高并发路径优先使用 GetChanClientListAsync。</remarks>
        /// <param name="chan">频道名</param>
        /// <returns></returns>
        IEnumerable<string> GetChanClientList(string chan);

        /// <summary>
        /// 异步获取频道所有客户端id（测试）
        /// </summary>
        /// <param name="chan">频道名</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetChanClientListAsync(string chan);

        /// <summary>
        /// 清理频道的离线客户端（测试）。
        /// </summary>
        /// <remarks>同步方法会阻塞当前线程，建议仅用于兼容旧代码；新的服务端调用优先使用 ClearChanClientAsync。</remarks>
        /// <param name="chan">频道名</param>
        void ClearChanClient(string chan);

        /// <summary>
        /// 异步清理频道的离线客户端（测试）
        /// </summary>
        /// <param name="chan">频道名</param>
        Task ClearChanClientAsync(string chan);


        /// <summary>
        /// 获取所有频道和在线人数。
        /// </summary>
        /// <remarks>同步方法仅用于兼容旧调用方；高并发路径优先使用 GetChanListAsync。</remarks>
        /// <returns>频道名和在线人数</returns>
        IEnumerable<ChanOnlineInfo> GetChanList();

        /// <summary>
        /// 异步获取所有频道和在线人数
        /// </summary>
        /// <returns>频道名和在线人数</returns>
        Task<IEnumerable<ChanOnlineInfo>> GetChanListAsync();

        /// <summary>
        /// 获取用户参与的所有频道。
        /// </summary>
        /// <remarks>同步方法仅用于兼容旧调用方；高并发路径优先使用 GetChanListByClientIdAsync。</remarks>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        IEnumerable<string> GetChanListByClientId(string clientId);

        /// <summary>
        /// 异步获取用户参与的所有频道
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetChanListByClientIdAsync(string clientId);


        /// <summary>
        /// 获取频道的在线人数。
        /// </summary>
        /// <remarks>同步方法仅用于兼容旧调用方；高并发路径优先使用 GetChanOnlineAsync。</remarks>
        /// <param name="chan">频道名</param>
        /// <returns>在线人数</returns>
        long GetChanOnline(string chan);

        /// <summary>
        /// 异步获取频道的在线人数
        /// </summary>
        /// <param name="chan">频道名</param>
        /// <returns>在线人数</returns>
        Task<long> GetChanOnlineAsync(string chan);

        #endregion
    }
}
