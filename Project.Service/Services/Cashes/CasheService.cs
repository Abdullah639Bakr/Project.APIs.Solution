using Azure;
using Project.Core.Services.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Service.Services.Cashes
{
    public class CasheService : ICasheService
    {
        private readonly IDatabase _database;
        public CasheService(IConnectionMultiplexer redis)
        {
            _database= redis.GetDatabase();
        }
        public async Task<string> GetCasheKeyAsync(string key)
        {
            var casheResponse = await _database.StringGetAsync(key);
            if(casheResponse.IsNullOrEmpty)return null;
            return casheResponse.ToString();
        }

        public async Task SetCasheKeyAsync(string key, object rasponse, TimeSpan expireTime)
        {
            if(rasponse is null) return;
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await _database.StringSetAsync(key,JsonSerializer.Serialize(rasponse,options),expireTime);
        }
    }
}
