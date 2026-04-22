namespace LiteIM
{
    public static class StackExchangeRedisImConsts
    {
        /// <summary>
        /// 加入群聊的 redis 语句
        /// </summary>
        public const string JoinChan = "redis.call('HSET', KEYS[1], ARGV[1], '0') redis.call('HSET', KEYS[2], ARGV[2], '0') redis.call('HINCRBY', KEYS[3], ARGV[2], '1') return 1";

        /// <summary>
        /// 离开群聊的 redis 语句
        /// </summary>
        public const string LeaveChan = "for i = 2, #ARGV do redis.call('HDEL', KEYS[i + 1], ARGV[1]) redis.call('HDEL', KEYS[2], ARGV[i]) if redis.call('HINCRBY', KEYS[1], ARGV[i], '-1') <= 0 then redis.call('HDEL', KEYS[1], ARGV[i]) end end return 1";

        /// <summary>
        /// 离开所有群聊的 redis 语句
        /// </summary>
        public const string LeaveAllChan = "local chans = redis.call('HKEYS', KEYS[2]) for i = 1, #chans do local chan = chans[i] redis.call('HDEL', ARGV[2] .. chan, ARGV[1]) redis.call('HDEL', KEYS[2], chan) if redis.call('HINCRBY', KEYS[1], chan, '-1') <= 0 then redis.call('HDEL', KEYS[1], chan) end end return #chans";

        /// <summary>
        /// 清理频道离线客户端的 redis 语句
        /// </summary>
        public const string ClearChanClient = "local clients = redis.call('HKEYS', KEYS[1]) local offline = {} for i = 1, #clients do if redis.call('HEXISTS', KEYS[2], clients[i]) == 0 then offline[#offline + 1] = clients[i] end end if #offline > 0 then redis.call('HDEL', KEYS[1], unpack(offline)) end return offline";
    }
}