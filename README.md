## DotNetCore MicroService Platform MongoDB Access 

Generic, based on type names, wrapper of the MongoDB C# Sharp 2.++ driver (async).
Saves lots of time in implementation No Sql repo POCs. 
This package is based on breaking design/interface change, reingenearing and refactoring the [MongoDb Generic Repository](https://github.com/alexandre-spieser/mongodb-generic-repository).


### Nuget.org

- Nuget package [DotNet.Mps.MongoDb](https://www.nuget.org/packages/DotNetCore.Mps.MongoDb/)

# Version: 2.0.0
- supports **netstandard2.0**


### Initialization Example:

``` csharp
        ...
        string connectionString = @" mondo db connection string ";
        MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
        settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
        var mongoClient = new MongoClient(settings);
        var mongoDatabase = mongoClient.GetDatabase(dbName);

        var mongoDbContext = new MongoDbContext(mongoDatabase);
```
