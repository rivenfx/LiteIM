using System;
using System.Collections.Generic;
using System.Text;

namespace LiteIM
{
    public static class CsRedisCoreImConsts
    {

        /// <summary>
        /// 离开群聊的redis语句
        /// </summary>
        public const string LeaveChan = "if redis.call('HINCRBY', KEYS[1], '{0}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{0}') end return 1";
    }
}
