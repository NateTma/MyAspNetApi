namespace MyAspNetApi.Models;

public class MongoDBSettings {

    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PlaylistCollectionName { get; set; } = null!;
    public string LocationCollectionName { get; set; } = null!;

}