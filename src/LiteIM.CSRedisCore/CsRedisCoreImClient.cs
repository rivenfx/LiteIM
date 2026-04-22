
using CSRedis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public virtual async Task<IEnumerable<string>> GetClientListByOnlineAsync()
        {
            var keys = await _redis.HKeysAsync(
                _prefix.CSRedisCoreOnline()
                );
            return keys.Where(o => !string.IsNullOrWhiteSpace(o));
        }

        public virtual bool HasOnline(string clientId)
        {
            return _redis.HGet<int>(_prefix.CSRedisCoreOnline(), clientId) > 0;
        }

        public virtual async Task<bool> HasOnlineAsync(string clientId)
        {
            return await _redis.HGetAsync<int>(_prefix.CSRedisCoreOnline(), clientId) > 0;
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

        public virtual Task JoinChanAsync(string clientId, string chan)
        {
            return Task.WhenAll(
                _redis.HSetAsync(
                    _prefix.CSRedisCoreChan(chan),
                    clientId,
                    0),
                _redis.HSetAsync(
                    _prefix.CSRedisCoreClient(clientId),
                    chan,
                    0),
                _redis.HIncrByAsync(
                    _prefix.CSRedisCoreListChan(),
                    chan,
                    1));
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

        public virtual Task LeaveChanAsync(string clientId, params string[] chans)
        {
            if (chans?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var tasks = new List<Task>(chans.Length * 3);
            foreach (var chan in chans)
            {
                tasks.Add(_redis.HDelAsync(
                    _prefix.CSRedisCoreChan(chan),
                    clientId
                    ));
                tasks.Add(_redis.HDelAsync(
                    _prefix.CSRedisCoreClient(clientId),
                    chan
                    ));
                tasks.Add(_redis.EvalAsync(
                    string.Format(CsRedisCoreImConsts.LeaveChan, chan),
                    _prefix.CSRedisCoreListChan()
                    ));
            }

            return Task.WhenAll(tasks);
        }


        public virtual void LeaveChan(string clientId)
        {
            // 所有的聊天室
            var chans = this.GetChanListByClientId(clientId);
            // 离开所有聊天室
            this.LeaveChan(clientId, chans.ToArray());
        }

        public virtual async Task LeaveChanAsync(string clientId)
        {
            var chans = await this.GetChanListByClientIdAsync(clientId);
            await this.LeaveChanAsync(clientId, chans.ToArray());
        }

        public virtual IEnumerable<string> GetChanClientList(string chan)
        {
            return _redis.HKeys(
                _prefix.CSRedisCoreChan(chan)
                )
                .AsEnumerable();
        }

        public virtual async Task<IEnumerable<string>> GetChanClientListAsync(string chan)
        {
            return (await _redis.HKeysAsync(
                _prefix.CSRedisCoreChan(chan)
                ))
                .AsEnumerable();
        }


        public virtual void ClearChanClient(string chan)
        {
            var clients = this.GetChanClientList(chan).ToArray();

            var offline = new List<string>();
            var span = clients.AsSpan();
            var start = span.Length;
            while (start > 0)
            {
                start = start - 10;
                var length = 10;
                if (start < 0)
                {
                    length = start + 10;
                    start = 0;
                }
                var slice = span.Slice(start, length);
                var hvals = _redis.HMGet(
                    _prefix.CSRedisCoreOnline(),
                    slice.ToArray()
                    .Select(b => b.ToString())
                    .ToArray()
                    );
                for (var a = length - 1; a >= 0; a--)
                {
                    if (string.IsNullOrEmpty(hvals[a]))
                    {
                        offline.Add(span[start + a]);
                        span[start + a] = null;
                    }
                }
            }

            //删除离线订阅
            if (offline.Any())
            {
                _redis.HDel(
                    _prefix.CSRedisCoreChan(chan),
                    offline.ToArray()
                    );
            }
        }

        public virtual async Task ClearChanClientAsync(string chan)
        {
            var clients = (await this.GetChanClientListAsync(chan)).ToArray();
            if (!clients.Any())
            {
                return;
            }

            var offline = new List<string>();
            var start = clients.Length;
            while (start > 0)
            {
                start = start - 10;
                var length = 10;
                if (start < 0)
                {
                    length = start + 10;
                    start = 0;
                }
                var slice = new string[length];
                Array.Copy(clients, start, slice, 0, length);
                var hvals = await _redis.HMGetAsync(
                    _prefix.CSRedisCoreOnline(),
                    slice
                    .Select(b => b.ToString())
                    .ToArray()
                    );
                for (var a = length - 1; a >= 0; a--)
                {
                    if (string.IsNullOrEmpty(hvals[a]))
                    {
                        offline.Add(clients[start + a]);
                    }
                }
            }

            if (offline.Any())
            {
                await _redis.HDelAsync(
                    _prefix.CSRedisCoreChan(chan),
                    offline.ToArray()
                    );
            }
        }


        public virtual IEnumerable<ChanOnlineInfo> GetChanList()
        {
            var ret = _redis.HGetAll<long>(
                    _prefix.CSRedisCoreListChan()
                );
            return ret.Select(a => new ChanOnlineInfo(a.Key, a.Value));
        }

        public virtual async Task<IEnumerable<ChanOnlineInfo>> GetChanListAsync()
        {
            var ret = await _redis.HGetAllAsync<long>(
                    _prefix.CSRedisCoreListChan()
                );
            return ret.Select(a => new ChanOnlineInfo(a.Key, a.Value));
        }

        public virtual IEnumerable<string> GetChanListByClientId(string clientId)
        {
            return _redis.HKeys(
                _prefix.CSRedisCoreClient(clientId)
                ).AsEnumerable();
        }

        public virtual async Task<IEnumerable<string>> GetChanListByClientIdAsync(string clientId)
        {
            return (await _redis.HKeysAsync(
                _prefix.CSRedisCoreClient(clientId)
                )).AsEnumerable();
        }

        public virtual long GetChanOnline(string chan)
        {
            return _redis.HGet<long>(
                _prefix.CSRedisCoreListChan(),
                 chan);
        }

        public virtual Task<long> GetChanOnlineAsync(string chan)
        {
            return _redis.HGetAsync<long>(
                _prefix.CSRedisCoreListChan(),
                 chan);
        }
        #endregion


    }
}
