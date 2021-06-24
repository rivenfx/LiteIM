using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    public class NullImClient : IImClient
    {
        public static NullImClient Instance { get; private set; } = new NullImClient();

        public void ClearChanClient(string chan)
        {

        }

        public IEnumerable<string> GetChanClientList(string chan)
        {
            yield break;
        }

        public IEnumerable<(string chan, long online)> GetChanList()
        {
            yield break;
        }

        public IEnumerable<string> GetChanListByClientId(string clientId)
        {
            yield break;
        }

        public long GetChanOnline(string chan)
        {
            return 0;
        }

        public IEnumerable<string> GetClientListByOnline()
        {
            yield break;
        }

        public bool HasOnline(string clientId)
        {
            return false;
        }

        public void JoinChan(string clientId, string chan)
        {

        }

        public void LeaveChan(string clientId, params string[] chans)
        {

        }

        public void LeaveChan(string clientId)
        {

        }
    }
}