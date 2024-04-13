using DotNet.Mps.MongoDb;
using DotNet.Mps.MongoDb.Abstractions;
using Mps.MongoDb.DataAccess;
using Mps.MongoDb.POC.Configuration;

namespace Mps.MongoDb.POC.DbContext;

public class ServiceMongoDbContext : IServiceMongoDbContext
{
    public MongoDbContext MongoDbContext { get; }
    public IMongoDbDataAccess MongoDbDataAccess { get; }

    private readonly ILogger<ServiceMongoDbContext> _logger;


    public ServiceMongoDbContext(ILogger<ServiceMongoDbContext> logger,
        IMongoDbConfiguration mongoDbConfiguration)
    {
        _logger = logger;
        try
        {
            MongoDbContext = new MongoDbContext(mongoDbConfiguration.DbConnectionString, mongoDbConfiguration.DatabaseName);
            MongoDbDataAccess = new MongoDbDataAccess(MongoDbContext);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,"[ServiceMongoDbContext] failed create MongoDbDataAccess for DB {DatabaseName}", mongoDbConfiguration.DatabaseName);
            throw;
        }
    }
}
