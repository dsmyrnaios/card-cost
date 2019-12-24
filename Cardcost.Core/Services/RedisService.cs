using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Core.Services.interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Cardcost.Core.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly int _redisPort;

        private ConnectionMultiplexer _redis;
        private readonly ILogger<CardService> _logger;

        public RedisService(IConfiguration config, ILogger<CardService> logger)
        {
            _redisHost = config["Redis:Host"];
            _redisPort = Convert.ToInt32(config["Redis:Port"]);

            //_redis = redis;
            _logger = logger;
        }

        public void Connect()
        {
            try
            {
                var configString = $"{_redisHost}:{_redisPort},connectRetry=5"; //the number of the retries for redis
                _redis = ConnectionMultiplexer.Connect(configString);
            }
            catch (RedisConnectionException err)
            {
                _logger.LogError(err.ToString());
                throw err;
            }
            _logger.LogDebug("Connected to Redis");
        }

        public async Task Set(string key, string value)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(key, value);
        }

        public async Task<string> Get(string key)
        {
            var db = _redis.GetDatabase();
            return await db.StringGetAsync(key);
        }
    }
}
