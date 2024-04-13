using Mps.MongoDb.POC;
using Mps.MongoDb.POC.Configuration;
using Mps.MongoDb.POC.DbContext;
using Mps.MongoDb.POC.Repository;

var builder = Host.CreateApplicationBuilder(args);

var mongoDbConfiguration = builder.Configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IMongoDbConfiguration>(mongoDbConfiguration);


builder.Services.AddTransient<IServiceMongoDbContext, ServiceMongoDbContext>();

builder.Services.AddTransient<IOrderRepository, OrderRepository>();


var host = builder.Build();
host.Run();
