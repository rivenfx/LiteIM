
using CSRedis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteIM.Extensions;

namespace LiteIM
{
    public class CsRedisCoreImClient : IImClient
    {
        protected readonly string _prefix;
        protected readonly CSRedisClient _redis;

        protected readonly CsRedisCoreImClientOptions _options;

        public CsRedisCoreImClient(CsRedisCoreImClientOptions options)
        {
            _options = options;

            _prefix = _options.Prefix;
            _redis = _options.Redis;
        }


        public virtual IEnumerable<string> GetClientListByOnline()
        {
            return _redis.HKeys(
                _prefix.Online()
                ).Where(o => !string.IsNullOrWhiteSpace(o));
        }

        public virtual bool HasOnline(string clientId)
        {
            return _redis.HGet<int>(_prefix.Online(), clientId) > 0;
        }



        #region 群聊频道

        public virtual void JoinChan(string clientId, string chan)
        {
            using (var pipe = _redis.StartPipe())
            {
                pipe.HSet(
                    _prefix.Chan(chan),
                    clientId,
                    0);
                pipe.HSet(
                    _prefix.Client(clientId),
                    chan,
                    0);
                pipe.HIncrBy(
                    _prefix.ListChan(),
                    chan,
                    1);
                pipe.EndPipe();
            }
        }

        public virtual void LeaveChan(string clientId, params string[] chans)
        {
            if (chans?.Any() != true)
            {
                return;
            }
            using (var pipe = _redis.StartPipe())
            {
                foreach (var chan in chans)
                {
                    pipe.HDel(
                        _prefix.Chan(chan),
                        clientId
                        );
                    pipe.HDel(
                        _prefix.Client(clientId),
                        chan
                        );
                    pipe.Eval(
                        string.Format(CsRedisCoreImConsts.LeaveChan, chan),
                        _prefix.ListChan()
                        );
                }
                pipe.EndPipe();
            }
        }


        public virtual void LeaveChan(string clientId)
        {
            // 所有的聊天室
            var chans = this.GetChanListByClientId(clientId);
            // 离开所有聊天室
            this.LeaveChan(clientId, chans.ToArray());
        }

        public virtual IEnumerable<string> GetChanClientList(string chan)
        {
            return _redis.HKeys(
                _prefix.Chan(chan)
                )
                .AsEnumerable();
        }


        public virtual void ClearChanClient(string chan)
        {
            throw new NotImplementedException();
        }


        public virtual IEnumerable<(string chan, long online)> GetChanList()
        {
            var ret = _redis.HGetAll<long>(
                    _prefix.ListChan()
                );
            return ret.Select(a => (a.Key, a.Value));
        }

        public virtual IEnumerable<string> GetChanListByClientId(string clientId)
        {
            return _redis.HKeys(
                _prefix.Client(clientId)
                ).AsEnumerable();
        }

        public virtual long GetChanOnline(string chan)
        {
            return _redis.HGet<long>(
                _prefix.ListChan(),
                 chan);
        }

        #endregion


    }
}
