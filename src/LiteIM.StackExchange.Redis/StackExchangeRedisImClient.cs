using LiteIM.Extensions;
using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LiteIM
{
    public class StackExchangeRedisImClient : IImClient
    {
        protected readonly string _prefix;
        protected readonly IDatabase _redis;

        protected readonly IStackExchangeRedisImClientOptions _options;

        public StackExchangeRedisImClient(IStackExchangeRedisImClientOptions options)
        {
            _options = options;
            if (_options.Database == null)
            {
                throw new ArgumentNullException(nameof(options.Redis), "IStackExchangeRedisImClientOptions Database 属性为空!");
            }

            _prefix = (_options.Prefix ?? string.Empty).Trim();
            _redis = _options.Database;
        }

        public virtual IEnumerable<string> GetClientListByOnline()
        {
            return _redis.HashKeys(_prefix.StackExchangeRedisOnline())
                .Select(o => (string)o)
                .Where(o => !string.IsNullOrWhiteSpace(o));
        }

        public virtual async Task<IEnumerable<string>> GetClientListByOnlineAsync()
        {
            return (await _redis.HashKeysAsync(_prefix.StackExchangeRedisOnline()))
                .Select(o => (string)o)
                .Where(o => !string.IsNullOrWhiteSpace(o));
        }

        public virtual bool HasOnline(string clientId)
        {
            return (long)_redis.HashGet(_prefix.StackExchangeRedisOnline(), clientId) > 0;
        }

        public virtual async Task<bool> HasOnlineAsync(string clientId)
        {
            return (long)await _redis.HashGetAsync(_prefix.StackExchangeRedisOnline(), clientId) > 0;
        }

        #region 群聊频道

        public virtual void JoinChan(string clientId, string chan)
        {
            _redis.ScriptEvaluate(
                StackExchangeRedisImConsts.JoinChan,
                new RedisKey[]
                {
                    _prefix.StackExchangeRedisChan(chan),
                    _prefix.StackExchangeRedisClient(clientId),
                    _prefix.StackExchangeRedisListChan()
                },
                new RedisValue[]
                {
                    clientId,
                    chan
                });
        }

        public virtual Task JoinChanAsync(string clientId, string chan)
        {
            return _redis.ScriptEvaluateAsync(
                StackExchangeRedisImConsts.JoinChan,
                new RedisKey[]
                {
                    _prefix.StackExchangeRedisChan(chan),
                    _prefix.StackExchangeRedisClient(clientId),
                    _prefix.StackExchangeRedisListChan()
                },
                new RedisValue[]
                {
                    clientId,
                    chan
                });
        }

        public virtual void LeaveChan(string clientId, params string[] chans)
        {
            if (chans?.Any() != true)
            {
                return;
            }

            var keys = new RedisKey[chans.Length + 2];
            keys[0] = _prefix.StackExchangeRedisListChan();
            keys[1] = _prefix.StackExchangeRedisClient(clientId);
            for (var index = 0; index < chans.Length; index++)
            {
                keys[index + 2] = _prefix.StackExchangeRedisChan(chans[index]);
            }

            var values = new RedisValue[chans.Length + 1];
            values[0] = clientId;
            for (var index = 0; index < chans.Length; index++)
            {
                values[index + 1] = chans[index];
            }

            _redis.ScriptEvaluate(
                StackExchangeRedisImConsts.LeaveChan,
                keys,
                values);
        }

        public virtual Task LeaveChanAsync(string clientId, params string[] chans)
        {
            if (chans?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var keys = new RedisKey[chans.Length + 2];
            keys[0] = _prefix.StackExchangeRedisListChan();
            keys[1] = _prefix.StackExchangeRedisClient(clientId);
            for (var index = 0; index < chans.Length; index++)
            {
                keys[index + 2] = _prefix.StackExchangeRedisChan(chans[index]);
            }

            var values = new RedisValue[chans.Length + 1];
            values[0] = clientId;
            for (var index = 0; index < chans.Length; index++)
            {
                values[index + 1] = chans[index];
            }

            return _redis.ScriptEvaluateAsync(
                StackExchangeRedisImConsts.LeaveChan,
                keys,
                values);
        }

        public virtual void LeaveChan(string clientId)
        {
            _redis.ScriptEvaluate(
                StackExchangeRedisImConsts.LeaveAllChan,
                new RedisKey[]
                {
                    _prefix.StackExchangeRedisListChan(),
                    _prefix.StackExchangeRedisClient(clientId)
                },
                new RedisValue[]
                {
                    clientId,
                    _prefix.StackExchangeRedisChan(string.Empty)
                });
        }

        public virtual async Task LeaveChanAsync(string clientId)
        {
            await _redis.ScriptEvaluateAsync(
                StackExchangeRedisImConsts.LeaveAllChan,
                new RedisKey[]
                {
                    _prefix.StackExchangeRedisListChan(),
                    _prefix.StackExchangeRedisClient(clientId)
                },
                new RedisValue[]
                {
                    clientId,
                    _prefix.StackExchangeRedisChan(string.Empty)
                });
        }

        public virtual IEnumerable<string> GetChanClientList(string chan)
        {
            return _redis.HashKeys(_prefix.StackExchangeRedisChan(chan))
                .Select(o => (string)o);
        }

        public virtual async Task<IEnumerable<string>> GetChanClientListAsync(string chan)
        {
            return (await _redis.HashKeysAsync(_prefix.StackExchangeRedisChan(chan)))
                .Select(o => (string)o);
        }

        public virtual void ClearChanClient(string chan)
        {
            _redis.ScriptEvaluate(
                StackExchangeRedisImConsts.ClearChanClient,
                new RedisKey[]
                {
                    _prefix.StackExchangeRedisChan(chan),
                    _prefix.StackExchangeRedisOnline()
                },
                Array.Empty<RedisValue>());
        }

        public virtual async Task ClearChanClientAsync(string chan)
        {
            await _redis.ScriptEvaluateAsync(
                StackExchangeRedisImConsts.ClearChanClient,
                new RedisKey[]
                {
                    _prefix.StackExchangeRedisChan(chan),
                    _prefix.StackExchangeRedisOnline()
                },
                Array.Empty<RedisValue>());
        }

        public virtual IEnumerable<ChanOnlineInfo> GetChanList()
        {
            return _redis.HashGetAll(_prefix.StackExchangeRedisListChan())
                .Select(a => new ChanOnlineInfo(a.Name, (long)a.Value));
        }

        public virtual async Task<IEnumerable<ChanOnlineInfo>> GetChanListAsync()
        {
            return (await _redis.HashGetAllAsync(_prefix.StackExchangeRedisListChan()))
                .Select(a => new ChanOnlineInfo(a.Name, (long)a.Value));
        }

        public virtual IEnumerable<string> GetChanListByClientId(string clientId)
        {
            return _redis.HashKeys(_prefix.StackExchangeRedisClient(clientId))
                .Select(a => (string)a);
        }

        public virtual async Task<IEnumerable<string>> GetChanListByClientIdAsync(string clientId)
        {
            return (await _redis.HashKeysAsync(_prefix.StackExchangeRedisClient(clientId)))
                .Select(a => (string)a);
        }

        public virtual long GetChanOnline(string chan)
        {
            return (long)_redis.HashGet(
                _prefix.StackExchangeRedisListChan(),
                chan);
        }

        public virtual async Task<long> GetChanOnlineAsync(string chan)
        {
            return (long)await _redis.HashGetAsync(
                _prefix.StackExchangeRedisListChan(),
                chan);
        }

        #endregion
    }
}