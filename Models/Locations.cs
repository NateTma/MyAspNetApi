using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAspNetApi.Models;

public class Locations{

    [BsonId]                               //converts the Id to an objectId
    [BsonRepresentation(BsonType.ObjectId)]//within MongoDb
    public string? Id { get; set; }

    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string L_name { get; set; } = null!;//location name

}