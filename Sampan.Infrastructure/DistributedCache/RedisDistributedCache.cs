using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sampan.Common.Util;
using StackExchange.Redis;

namespace Sampan.Infrastructure.DistributedCache
{
    public class RedisDistributedCache : IDistributedCache
    {
        private readonly string _connenctionString;
        private readonly string _connenctionPwd;

        private volatile ConnectionMultiplexer _connection;

        private readonly object _redisLock = new object();
        private readonly IDatabaseAsync _database;

        public RedisDistributedCache()
        {
            _connenctionString = Appsettings.app(new string[] {"ConnectionStrings", "RedisConnectionString"}); //获取连接字符串

            _connenctionPwd = Appsettings.app(new string[] {"ConnectionStrings", "RedisPwd"}); //获取连接密码
            if (string.IsNullOrWhiteSpace(_connenctionString))
            {
                throw new ArgumentException("redis config is empty", nameof(_connenctionString));
            }

            _connection = GetConnection();
            _database = _connection.GetDatabase();
        }

        /// <summary>
        /// 核心代码，获取连接实例
        /// 通过双if 夹lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnection()
        {
            //如果已经连接实例，直接返回
            if (_connection != null && _connection.IsConnected)
            {
                return _connection;
            }

            //加锁，防止异步编程中，出现单例无效的问题
            lock (_redisLock)
            {
                //释放redis连接
                _connection?.Dispose();

                try
                {
                    var config = new ConfigurationOptions
                    {
                        AbortOnConnectFail = false,
                        AllowAdmin = true,
                        ConnectTimeout = 15000, //改成15s
                        SyncTimeout = 5000,
                        EndPoints = {_connenctionString} // connectionString 为IP:Port 如”192.168.2.110:6379”
                    };
                    if (!string.IsNullOrEmpty(_connenctionPwd))
                        config.Password = DesEncryptUtil.Decrypt(_connenctionPwd); //Redis数据库密码;
                    _connection = ConnectionMultiplexer.Connect(config);
                }
                catch (Exception)
                {
                    throw new Exception("Redis服务未启用，请开启该服务，并且请注意端口号");
                }
            }

            return _connection;
        }

        public async Task<bool> ExistAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public List<string> GetKeysAsync(string prefix = null)
        {
            var server = _connection.GetServer(_connection.GetEndPoints()[0]);
            var keys = server.Keys(pattern: $"{prefix}*");
            var result = keys.Select(a => a.ToString()).ToList();
            return result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                return ByteSerializeUtil.Deserialize<T>(value);
            }

            return default;
        }

        public async Task<string> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value;
        }

        public async Task SetAsync(string key, object value, TimeSpan? expiryTime = null)
        {
            if (value is string)
            {
                await _database.StringSetAsync(key, value.ToString(), expiryTime);
            }
            else
            {
                await _database.StringSetAsync(key, ByteSerializeUtil.Serialize(value), expiryTime);
            }
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task RemoveAsync(List<string> keys)
        {
            await _database.KeyDeleteAsync(keys.Select(a => (RedisKey) a).ToArray());
        }

        public async Task ClearAsync()
        {
            var keys = GetKeysAsync();
            await RemoveAsync(keys);
        }

        public async Task RemoveByKeyPrefixAsync(string keyPrefix)
        {
            var keys = GetKeysAsync(keyPrefix);
            await RemoveAsync(keys);
        }
    }
}