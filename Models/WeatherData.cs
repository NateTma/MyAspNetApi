using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAspNetApi.Models;

public class WeatherData{

    [BsonId]                               //converts the Id to an objectId
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string LocationName {get; set; } = null!;

    public DateTime Date { get; set; }

    public double TemperatureC { get; set; }

    public double TemperatureF => 32 + (TemperatureC/0.5556);

    public int Humidity { get; set; }

    public string Description { get; set; } = null!;

}