namespace Mps.MongoDb.POC.Configuration;


public class MongoDbConfiguration : IMongoDbConfiguration
{
    public string DatabaseName { get; set; }
    public string DbConnectionString { get; set; }
}