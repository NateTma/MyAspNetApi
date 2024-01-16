using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MyAspNetApi.Data // Replace "YourProject" with your actual project namespace
{
    public class MongoDBContext
    {
        private readonly Dictionary<string, IMongoDatabase> _databases;
        private readonly IMongoClient _client;

        public MongoDBContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDB:ConnectionURI");
            _client = new MongoClient(connectionString);
            _databases = new Dictionary<string, IMongoDatabase>();
        }

        public IMongoDatabase GetDatabase(string databaseName)
        {
            if (_databases.ContainsKey(databaseName))
            {
                return _databases[databaseName];
            }

            var database = _client.GetDatabase(databaseName);
            _databases.Add(databaseName, database);
            return database;
        }

        public IMongoCollection<T> GetCollection<T>(string databaseName, string collectionName)
        {
            var database = GetDatabase(databaseName);
            return database.GetCollection<T>(collectionName);
        }

    }
}

