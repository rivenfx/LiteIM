
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

        protected readonly ICsRedisCoreImClientOptions _options;

        public CsRedisCoreImClient(ICsRedisCoreImClientOptions options)
        {
            _options = options;
            if (_options.Redis == null)
            {
                throw new ArgumentNullException(nameof(options.Redis), "ICsRedisCoreImClientOptions Redis 属性为空!");
            }


            _prefix = (_options.Prefix ?? string.Empty).Trim();
            _redis = _options.Redis;
        }


        public virtual IEnumerable<string> GetClientListByOnline()
        {
            return _redis.HKeys(
                _prefix.CSRedisCoreOnline()
                ).Where(o => !string.IsNullOrWhiteSpace(o));
        }

        public virtual bool HasOnline(string clientId)
        {
            return _redis.HGet<int>(_prefix.CSRedisCoreOnline(), clientId) > 0;
        }



        #region 群聊频道

        public virtual void JoinChan(string clientId, string chan)
        {
            using (var pipe = _redis.StartPipe())
            {
                pipe.HSet(
                    _prefix.CSRedisCoreChan(chan),
                    clientId,
                    0);
                pipe.HSet(
                    _prefix.CSRedisCoreClient(clientId),
                    chan,
                    0);
                pipe.HIncrBy(
                    _prefix.CSRedisCoreListChan(),
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
                        _prefix.CSRedisCoreChan(chan),
                        clientId
                        );
                    pipe.HDel(
                        _prefix.CSRedisCoreClient(clientId),
                        chan
                        );
                    pipe.Eval(
                        string.Format(CsRedisCoreImConsts.LeaveChan, chan),
                        _prefix.CSRedisCoreListChan()
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
                _prefix.CSRedisCoreChan(chan)
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
                    _prefix.CSRedisCoreListChan()
                );
            return ret.Select(a => (a.Key, a.Value));
        }

        public virtual IEnumerable<string> GetChanListByClientId(string clientId)
        {
            return _redis.HKeys(
                _prefix.CSRedisCoreClient(clientId)
                ).AsEnumerable();
        }

        public virtual long GetChanOnline(string chan)
        {
            return _redis.HGet<long>(
                _prefix.CSRedisCoreListChan(),
                 chan);
        }

        #endregion


    }
}
