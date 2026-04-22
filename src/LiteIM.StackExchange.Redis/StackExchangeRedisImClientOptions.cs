using StackExchange.Redis;

using System;

namespace LiteIM
{
    public class StackExchangeRedisImClientOptions : ImClientOptions, IStackExchangeRedisImClientOptions
    {
        public IConnectionMultiplexer Redis { get; set; }

        public IDatabase Database => _database.Value;

        private Lazy<IDatabase> _database;

        public StackExchangeRedisImClientOptions(IDatabase database)
        {
            _database = new Lazy<IDatabase>(() => database);
        }

        public StackExchangeRedisImClientOptions()
        {
            _database = new Lazy<IDatabase>(() =>
            {
                return Redis?.GetDatabase();
            });
        }
    }
}