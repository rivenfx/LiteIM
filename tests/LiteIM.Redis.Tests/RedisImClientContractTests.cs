using System;
using System.Linq;
using System.Threading.Tasks;

using CSRedis;

using LiteIM.Extensions;

using StackExchange.Redis;

using Xunit;

namespace LiteIM.Redis.Tests
{
    public class RedisImClientContractTests : IClassFixture<RedisTestFixture>
    {
        private readonly RedisTestFixture _fixture;

        public RedisImClientContractTests(RedisTestFixture fixture)
        {
            _fixture = fixture;
        }

        public static TheoryData<string> Providers => new TheoryData<string>
        {
            nameof(CsRedisCoreImClient),
            nameof(StackExchangeRedisImClient)
        };

        [Theory]
        [MemberData(nameof(Providers))]
        public void JoinChan_ShouldTrackMembershipAndOnlineCounts(string providerName)
        {
            using var context = _fixture.CreateContext(providerName);

            context.SetOnline("client-1", 1);
            context.Client.JoinChan("client-1", "room-a");

            Assert.True(context.Client.HasOnline("client-1"));
            Assert.Contains("client-1", context.Client.GetClientListByOnline());
            Assert.Contains("room-a", context.Client.GetChanListByClientId("client-1"));
            Assert.Contains("client-1", context.Client.GetChanClientList("room-a"));
            Assert.Equal(1, context.Client.GetChanOnline("room-a"));

            var chan = Assert.Single(context.Client.GetChanList());
            Assert.Equal("room-a", chan.Chan);
            Assert.Equal(1, chan.Online);
        }

        [Theory]
        [MemberData(nameof(Providers))]
        public async Task JoinChanAsync_ShouldTrackMembershipAndOnlineCounts(string providerName)
        {
            using var context = _fixture.CreateContext(providerName);

            context.SetOnline("client-1", 1);
            await context.Client.JoinChanAsync("client-1", "room-a");

            Assert.True(await context.Client.HasOnlineAsync("client-1"));
            Assert.Contains("client-1", await context.Client.GetClientListByOnlineAsync());
            Assert.Contains("room-a", await context.Client.GetChanListByClientIdAsync("client-1"));
            Assert.Contains("client-1", await context.Client.GetChanClientListAsync("room-a"));
            Assert.Equal(1, await context.Client.GetChanOnlineAsync("room-a"));

            var chan = Assert.Single(await context.Client.GetChanListAsync());
            Assert.Equal("room-a", chan.Chan);
            Assert.Equal(1, chan.Online);
        }

        [Theory]
        [MemberData(nameof(Providers))]
        public void LeaveChan_ShouldRemoveMembershipAndDecrementChannelCount(string providerName)
        {
            using var context = _fixture.CreateContext(providerName);

            context.SetOnline("client-1", 1);
            context.Client.JoinChan("client-1", "room-a");
            context.Client.JoinChan("client-1", "room-b");

            context.Client.LeaveChan("client-1", "room-a");

            Assert.DoesNotContain("room-a", context.Client.GetChanListByClientId("client-1"));
            Assert.DoesNotContain("client-1", context.Client.GetChanClientList("room-a"));
            Assert.Equal(0, context.Client.GetChanOnline("room-a"));
            Assert.Equal(1, context.Client.GetChanOnline("room-b"));
            Assert.Contains("room-b", context.Client.GetChanListByClientId("client-1"));
        }

        [Theory]
        [MemberData(nameof(Providers))]
        public void LeaveChanAll_ShouldRemoveClientFromAllChannels(string providerName)
        {
            using var context = _fixture.CreateContext(providerName);

            context.SetOnline("client-1", 1);
            context.Client.JoinChan("client-1", "room-a");
            context.Client.JoinChan("client-1", "room-b");

            context.Client.LeaveChan("client-1");

            Assert.Empty(context.Client.GetChanListByClientId("client-1"));
            Assert.DoesNotContain("client-1", context.Client.GetChanClientList("room-a"));
            Assert.DoesNotContain("client-1", context.Client.GetChanClientList("room-b"));
            Assert.Equal(0, context.Client.GetChanOnline("room-a"));
            Assert.Equal(0, context.Client.GetChanOnline("room-b"));
        }

        [Theory]
        [MemberData(nameof(Providers))]
        public async Task LeaveChanAsync_ShouldRemoveClientFromAllChannels(string providerName)
        {
            using var context = _fixture.CreateContext(providerName);

            context.SetOnline("client-1", 1);
            await context.Client.JoinChanAsync("client-1", "room-a");
            await context.Client.JoinChanAsync("client-1", "room-b");

            await context.Client.LeaveChanAsync("client-1");

            Assert.Empty(await context.Client.GetChanListByClientIdAsync("client-1"));
            Assert.DoesNotContain("client-1", await context.Client.GetChanClientListAsync("room-a"));
            Assert.DoesNotContain("client-1", await context.Client.GetChanClientListAsync("room-b"));
            Assert.Equal(0, await context.Client.GetChanOnlineAsync("room-a"));
            Assert.Equal(0, await context.Client.GetChanOnlineAsync("room-b"));
        }

        [Theory]
        [MemberData(nameof(Providers))]
        public void ClearChanClient_ShouldRemoveOfflineClientsOnly(string providerName)
        {
            using var context = _fixture.CreateContext(providerName);

            context.SetOnline("online-client", 1);
            context.Client.JoinChan("online-client", "room-a");
            context.Client.JoinChan("offline-client", "room-a");

            context.Client.ClearChanClient("room-a");

            var clients = context.Client.GetChanClientList("room-a").OrderBy(a => a).ToArray();
            Assert.Single(clients);
            Assert.Equal("online-client", clients[0]);
        }

        [Theory]
        [MemberData(nameof(Providers))]
        public async Task ClearChanClientAsync_ShouldRemoveOfflineClientsOnly(string providerName)
        {
            using var context = _fixture.CreateContext(providerName);

            context.SetOnline("online-client", 1);
            await context.Client.JoinChanAsync("online-client", "room-a");
            await context.Client.JoinChanAsync("offline-client", "room-a");

            await context.Client.ClearChanClientAsync("room-a");

            var clients = (await context.Client.GetChanClientListAsync("room-a")).OrderBy(a => a).ToArray();
            Assert.Single(clients);
            Assert.Equal("online-client", clients[0]);
        }

        [Fact]
        public void CsRedisCoreClient_ShouldThrowWhenRedisIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new CsRedisCoreImClient(new CsRedisCoreImClientOptions()));

            Assert.Equal("Redis", exception.ParamName);
        }

        [Fact]
        public void StackExchangeRedisClient_ShouldThrowWhenRedisIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new StackExchangeRedisImClient(new StackExchangeRedisImClientOptions()));

            Assert.Equal("Redis", exception.ParamName);
        }
    }

    public sealed class RedisTestFixture : IDisposable
    {
        private const string ConnectionString = "127.0.0.1:6379,password=bb123456,defaultDatabase=10,abortConnect=false";
        private readonly ConnectionMultiplexer _multiplexer;

        public RedisTestFixture()
        {
            _multiplexer = ConnectionMultiplexer.Connect(ConnectionString);
        }

        public TestContext CreateContext(string providerName)
        {
            var prefix = $"test:{providerName}:{Guid.NewGuid():N}:";
            var database = _multiplexer.GetDatabase(10);
            var client = providerName switch
            {
                nameof(CsRedisCoreImClient) => (IImClient)new CsRedisCoreImClient(new CsRedisCoreImClientOptions
                {
                    Prefix = prefix,
                    Redis = new CSRedisClient(ConnectionString)
                }),
                nameof(StackExchangeRedisImClient) => new StackExchangeRedisImClient(new StackExchangeRedisImClientOptions
                {
                    Prefix = prefix,
                    Redis = _multiplexer
                }),
                _ => throw new ArgumentOutOfRangeException(nameof(providerName), providerName, null)
            };

            return new TestContext(prefix, database, client);
        }

        public void Dispose()
        {
            _multiplexer.Dispose();
        }
    }

    public sealed class TestContext : IDisposable
    {
        private readonly string _prefix;
        private readonly IDatabase _database;

        public TestContext(string prefix, IDatabase database, IImClient client)
        {
            _prefix = prefix;
            _database = database;
            Client = client;
        }

        public IImClient Client { get; }

        public void SetOnline(string clientId, long value)
        {
            _database.HashSet(GetOnlineKey(), clientId, value);
        }

        public void Dispose()
        {
            var endpoints = _database.Multiplexer.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _database.Multiplexer.GetServer(endpoint);
                foreach (var key in server.Keys(_database.Database, $"n:{_prefix}LiteIM,*"))
                {
                    _database.KeyDelete(key);
                }
            }
        }

        private string GetOnlineKey()
        {
            return $"n:{_prefix}LiteIM,c:{string.Empty.Online()}";
        }
    }
}