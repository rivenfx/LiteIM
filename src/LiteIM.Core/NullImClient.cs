using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiteIM
{
    /// <summary>
    /// 空实现
    /// </summary>
    public class NullImClient : IImClient
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static NullImClient Instance { get; private set; } = new NullImClient();

        public void ClearChanClient(string chan)
        {

        }

        public Task ClearChanClientAsync(string chan)
        {
            return Task.CompletedTask;
        }

        public IEnumerable<string> GetChanClientList(string chan)
        {
            yield break;
        }

        public Task<IEnumerable<string>> GetChanClientListAsync(string chan)
        {
            return Task.FromResult(GetChanClientList(chan));
        }

        public IEnumerable<ChanOnlineInfo> GetChanList()
        {
            yield break;
        }

        public Task<IEnumerable<ChanOnlineInfo>> GetChanListAsync()
        {
            return Task.FromResult(GetChanList());
        }

        public IEnumerable<string> GetChanListByClientId(string clientId)
        {
            yield break;
        }

        public Task<IEnumerable<string>> GetChanListByClientIdAsync(string clientId)
        {
            return Task.FromResult(GetChanListByClientId(clientId));
        }

        public long GetChanOnline(string chan)
        {
            return 0;
        }

        public Task<long> GetChanOnlineAsync(string chan)
        {
            return Task.FromResult(0L);
        }

        public IEnumerable<string> GetClientListByOnline()
        {
            yield break;
        }

        public Task<IEnumerable<string>> GetClientListByOnlineAsync()
        {
            return Task.FromResult(GetClientListByOnline());
        }

        public bool HasOnline(string clientId)
        {
            return false;
        }

        public Task<bool> HasOnlineAsync(string clientId)
        {
            return Task.FromResult(false);
        }

        public void JoinChan(string clientId, string chan)
        {

        }

        public Task JoinChanAsync(string clientId, string chan)
        {
            return Task.CompletedTask;
        }

        public void LeaveChan(string clientId, params string[] chans)
        {

        }

        public Task LeaveChanAsync(string clientId, params string[] chans)
        {
            return Task.CompletedTask;
        }

        public void LeaveChan(string clientId)
        {

        }

        public Task LeaveChanAsync(string clientId)
        {
            return Task.CompletedTask;
        }
    }
}
