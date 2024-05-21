using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using Application.Interfaces;

namespace Application.Services;

public class CacheService : ICacheService
{
    private IDatabase _db;

    public CacheService()
    {
        ConfigureRedis();
    }

    private void ConfigureRedis()
    {
        ConnectionMultiplexer cluster = ConnectionMultiplexer.Connect(Configuration.ConnectionStrings.Redis);

        _db = cluster.GetDatabase();

    }

    public T GetData<T>(string key)
    {
        var value = _db.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        return default;
    }

    public bool SetData<T>(string key, T value)
    {
        var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value));
        return isSet;
    }

    public object RemoveData(string key)
    {
        bool _isKeyExist = _db.KeyExists(key);
        if (_isKeyExist == true)
        {
            return _db.KeyDelete(key);
        }

        return false;
    }
}