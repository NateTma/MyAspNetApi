using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAspNetApi.Models;

public class Playlist{

    [BsonId]                               //converts the Id to an objectId
    [BsonRepresentation(BsonType.ObjectId)]//within MongoDB
    public string? Id { get; set; }

    public string username { get; set; } = null!;

    [BsonElement("items")]
    [JsonPropertyName("items")]//returns data to clients as Json
    public List<string> movieIds { get; set; } = null!;

}