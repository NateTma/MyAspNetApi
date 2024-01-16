using MyAspNetApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAspNetApi.Services;

public class MongoDBService
{
    private readonly Dictionary<string, Dictionary<Type, object>> _databaseCollections;

    public MongoDBService(IConfiguration configuration, IOptions<Dictionary<string, MongoDBSettings>> databaseSettings)
    {
        _databaseCollections = new Dictionary<string, Dictionary<Type, object>>();
        var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDB:Database1:ConnectionURI"));
        var database1 = mongoClient.GetDatabase(configuration["MongoDB:Database1:DatabaseName"]);
        var database2 = mongoClient.GetDatabase(configuration["MongoDB:Database2:DatabaseName"]);

        var collection1 = database1.GetCollection<Playlist>(configuration["MongoDB:Database1:CollectionName"]);
        var collection2 = database2.GetCollection<Locations>(configuration["MongoDB:Database2:CollectionName"]);

        var collections1 = new Dictionary<Type, object> { { typeof(Playlist), collection1 } };
        var collections2 = new Dictionary<Type, object> { { typeof(Locations), collection2 } };

        _databaseCollections.Add("Database1", collections1);
        _databaseCollections.Add("Database2", collections2);
    }

    /* Create operation */
    public async Task CreateAsync<T>(string databaseName, T item)
    {
        var collection = GetCollection<T>(databaseName);
        await collection.InsertOneAsync(item);
    }

    /* Get operation */
    public async Task<List<T>> GetAsync<T>(string databaseName)
    {
        var collection = GetCollection<T>(databaseName);
        var filter = new BsonDocument();
        return await collection.Find(filter).ToListAsync();
    }

    /* Update operation */
    public async Task UpdateAsync<T>(string databaseName, string id, UpdateDefinition<T> updateDefinition)
    {
        var collection = GetCollection<T>(databaseName);
        var filter = Builders<T>.Filter.Eq("Id", id);
        await collection.UpdateOneAsync(filter, updateDefinition);
    }

    /* Delete operation */
    public async Task DeleteAsync<T>(string databaseName, string id)
    {
        var collection = GetCollection<T>(databaseName);
        var filter = Builders<T>.Filter.Eq("Id", id);
        await collection.DeleteOneAsync(filter);
    }

    private IMongoCollection<T> GetCollection<T>(string databaseName)
    {
        if (!_databaseCollections.TryGetValue(databaseName, out var collections))
        {
            throw new ArgumentException($"Database with name '{databaseName}' does not exist.");
        }

        var type = typeof(T);
        if (!collections.TryGetValue(type, out var collection))
        {
            throw new ArgumentException($"Collection for type '{type.Name}' does not exist in database '{databaseName}'.");
        }

        return (IMongoCollection<T>)collection;
    }
}

//fix controllers as well and finish up
