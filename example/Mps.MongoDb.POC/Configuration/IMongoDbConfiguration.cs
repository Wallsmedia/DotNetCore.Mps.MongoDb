namespace Mps.MongoDb.POC.Configuration;

public interface IMongoDbConfiguration
{
    string DatabaseName { get; }
    string DbConnectionString { get; }
}