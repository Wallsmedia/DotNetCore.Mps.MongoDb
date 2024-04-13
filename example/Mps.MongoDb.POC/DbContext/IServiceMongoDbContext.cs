using DotNet.Mps.MongoDb;
using DotNet.Mps.MongoDb.Abstractions;

namespace Mps.MongoDb.POC.DbContext;

public interface IServiceMongoDbContext
{
    MongoDbContext MongoDbContext { get; }
    IMongoDbDataAccess MongoDbDataAccess { get; }
}