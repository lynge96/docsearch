using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using Application.Interfaces;

// Denne klasse implementerer ICacheService-interfacet og fungerer som en service til at håndtere cache-operationer ved hjælp af Redis-databasen.
// CacheService er ansvarlig for at hente, gemme og fjerne data fra cachen.


namespace Application.Services;

public class CacheService : ICacheService
{
    private IDatabase _db;

    // Konstruktør der konfigurerer forbindelsen til Redis-databasen
    public CacheService()
    {
        ConfigureRedis();
    }

    // Metode til at konfigurere forbindelsen til Redis-databasen
    private void ConfigureRedis()
    {
        ConnectionMultiplexer cluster = ConnectionMultiplexer.Connect(Configuration.ConnectionStrings.Redis);

        _db = cluster.GetDatabase();

    }

    // Metode til at hente data fra cachen
    //Denne metode henter data fra cachen ved at bruge den angivne nøgle.
    //Hvis data findes i cachen, deserialiseres den til den ønskede type og returneres.
    public T GetData<T>(string key)
    {
        var value = _db.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        return default;
    }

    // Metode til at gemme data i cachen
    //Denne metode gemmer data i cachen ved at bruge den angivne nøgle og værdi.
    //Data serialiseres først til JSON-format, før den gemmes i cachen.
    public bool SetData<T>(string key, T value)
    {
        var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value));
        return isSet;
    }

    // Metode til at fjerne data fra cachen
    //Denne metode fjerner data fra cachen ved hjælp af den angivne nøgle. Hvis nøglen eksisterer i cachen,
    //slettes den og metoden returnerer sandt, ellers returneres falsk.
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