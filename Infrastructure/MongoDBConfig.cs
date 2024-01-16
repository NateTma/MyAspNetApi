using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
//using MyAspNetApi.Models;
//using MyAspNetApi.Services;

namespace MyAspNetApi.Infrastructure;

public class MongoDBConfig
{
    private readonly IConfiguration _configuration;

    public MongoDBConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IMongoDatabase GetDatabase(string databaseName)
    {
        string connectionString = _configuration.GetConnectionString(databaseName);
        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseName);
        
        return mongoDatabase;
    }
}