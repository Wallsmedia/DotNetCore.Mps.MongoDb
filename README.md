## DotNetCore MicroService Platform MongoDB Access 
This package is based on breaking and deep design/interface changes, including re-engineering and refactoring of the original *MongoDb Generic Repository*

Features:
- Generic
- GUID Id based only for structured documents.
- Collection names defined via attributes on type names
- Clear light wrapper of the MongoDB C# Sharp 2.++ driver (async).
- Allows still access to original Mongo DB client.
- Fully adopted for use in NetCorer DI 
- Saves lots of time in implementation of NoSql entity repositories. 




### Nuget.org

- Nuget package [DotNet.Mps.MongoDb](https://www.nuget.org/packages/DotNetCore.Mps.MongoDb/)

### Version: 2.0.1
- supports **netstandard2.0**
- updated driver version


# POC Example or How to 

See example source project: [MongoDB Access Example](https://github.com/Wallsmedia/DotNetCore.Mps.MongoDb/tree/master/example)

## Step - Nuget reference
Add to project

``` xml

  <ItemGroup>
    <PackageReference Include="DotNetCore.Mps.MongoDb" Version="2.0.*" />
  </ItemGroup>

```

## Step - Mongo DB Configuration


### Define MongoDB configuration interface - IMongoDbConfiguration.cs
``` csharp
public interface IMongoDbConfiguration
{
    string DatabaseName { get; }
    string DbConnectionString { get; }
}
```

### Define  MongoDB configuration - MongoDbConfiguration.cs
``` csharp
public class MongoDbConfiguration : IMongoDbConfiguration
{
    public string DatabaseName { get; set; }
    public string DbConnectionString { get; set; }
}
```


### Define MongoDB configuration 
The main purpose is to put *MongoDbConfiguration* instance into .NetCore DI container.

Here is *appsettings.json* fragment example. 

``` json
{


 "MongoDbConfiguration": {
    "DatabaseName": "docsmongodb",
    "DbConnectionString": "secrets-kv-mongodb-string-example"
  }

 }
```

Configuration initialization may depend from your project.
Here is an example that suitable for Hosted Service, Asp Net Core and Azure Functions (with .NetCore DI container)

``` csharp

var builder = Host.CreateApplicationBuilder(args);

var mongoDbConfiguration = builder.Configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>();

builder.Services.AddSingleton<IMongoDbConfiguration>(mongoDbConfiguration);



```

## Step - Service MongoDB Context

The main purpose is to define MongoDB context that can be used for access of document repositories.

### Define MongoDB context interface - IServiceMongoDbContext.cs
``` csharp

public interface IServiceMongoDbContext
{
    MongoDbContext MongoDbContext { get; }
    IMongoDbDataAccess MongoDbDataAccess { get; }
}

```

### Define Service MongoDB context implementation - ServiceMongoDbContext.cs 

``` csharp

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

```

### Add to .NetCore DI container initialization 
``` csharp
...

builder.Services.AddTransient<IServiceMongoDbContext,ServiceMongoDbContext>();
// Or use AddSingleton for simple components or single repository 

...

```

## Step - Define document Entity Models

Just for example.

``` csharp

[MongoDbCollection("orders")]
public class OrderStructured : StructuredDocument
{
    public int OrderNumber { get; set; }
    public Address ShippingAddress { get; set; }
    public List<Product> Products { get; set; }
    public OptionalOrderInfo OptionalInfo { get; set; }
}

```

``` csharp
[MongoDbCollection("customers")]
public class CustomerStructured : StructuredDocument
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }

    public string SomeExtraAdditionalInfo { get; set; }
    public string AdditionalInfo { get; set; }
    public Address Address { get; set; }

    public List<Guid> Orders { get; set; }
}


```

``` csharp
public class OptionalOrderInfo
{
    public string AdditionalInfo { get; set; }
    public List<string> ExtraAdditionalInfo { get; set; }
}

```

``` csharp
public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
}

```

``` csharp

public class Product
{
    public int ProductCode { get; set; }
    public string Name { get; set; }

}

```

## Step - Define repository/provider for working with entities 


### Define order functional interface - IOrderRepository.cs
``` csharp

public interface IOrderRepository
{
    Task AddCustomerAsync(CustomerStructured customer);
    Task AddOrderAsync(OrderStructured order);
    Task UpdateCustomerAsync(CustomerStructured customer);
    Task UpdateOrderAsync(OrderStructured order);
    Task<CustomerStructured> GetCustomerAsync(Guid id);
    Task<List<CustomerStructured>> GetCustomersAsync(string firstName);
    Task<OrderStructured> GetOrderAsync(Guid id);
    Task<List<OrderStructured>> GetOrdersAsync(int orderNumber);
}

```

### Define implementation  - OrderRepository.cs

``` csharp

public class OrderRepository : IOrderRepository
{
    private readonly IServiceMongoDbContext _serviceMongoDbContext;

    public OrderRepository(IServiceMongoDbContext serviceMongoDbContext)
    {
        _serviceMongoDbContext = serviceMongoDbContext;
    }

    public async Task AddCustomerAsync(CustomerStructured customerStructured)
    {
        await _serviceMongoDbContext
           .MongoDbDataAccess
           .AddOneAsync(customerStructured);
    }

    public async Task AddOrderAsync(OrderStructured order)
    {
        await _serviceMongoDbContext
           .MongoDbDataAccess
           .AddOneAsync(order);
    }

    public async Task UpdateCustomerAsync(CustomerStructured customerStructured)
    {
        await _serviceMongoDbContext
           .MongoDbDataAccess
           .UpdateOneAsync(customerStructured);
    }

    public async Task UpdateOrderAsync(OrderStructured order)
    {
        await _serviceMongoDbContext
           .MongoDbDataAccess
           .UpdateOneAsync(order);
    }

    public async Task<List<CustomerStructured>> GetCustomersAsync(string firstName)
    {

        var customers = await _serviceMongoDbContext
            .MongoDbDataAccess
            .GetAllAsync<CustomerStructured>(c => c.FirstName.StartsWith(firstName, StringComparison.InvariantCultureIgnoreCase));
        return customers;
    }

    public async Task<CustomerStructured> GetCustomerAsync(Guid id)
    {

        var customer = await _serviceMongoDbContext
            .MongoDbDataAccess
            .GetByIdAsync<CustomerStructured>(id);
        return customer;
    }

    public async Task<List<OrderStructured>> GetOrdersAsync(int orderNumber)
    {

        var orders = await _serviceMongoDbContext
            .MongoDbDataAccess
            .GetAllAsync<OrderStructured>(c => c.OrderNumber == orderNumber);
        return orders;
    }

    public async Task<OrderStructured> GetOrderAsync(Guid id)
    {

        var order = await _serviceMongoDbContext
            .MongoDbDataAccess
            .GetByIdAsync<OrderStructured>(id);
        return order;
    }

}


```


### Add to .NetCore DI container initialization 
``` csharp
...

builder.Services.AddTransient<IOrderRepository,OrderRepository>();
// Or use AddSingleton for simple components or single repository.

...

```

## Step - Example of using in hosted service


### Define hosted service
``` csharp

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IOrderRepository _orderRepo;
    private readonly Fixture _fixture;

    public Worker(ILogger<Worker> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepo = orderRepository;
        _fixture = new Fixture();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Add and Get examples to Mongo DB at: {time}", DateTimeOffset.Now);
        try
        {

            var order = _fixture.Create<OrderStructured>();
            order.Version = 1;
            await _orderRepo.AddOrderAsync(order);

            order.Version = 100;
            await _orderRepo.UpdateOrderAsync(order);


            var customer = _fixture.Create<CustomerStructured>();
            customer.Version = 1;
            await _orderRepo.AddCustomerAsync(customer);

            customer.Version = 100;
            await _orderRepo.UpdateCustomerAsync(customer);


            var orders = _orderRepo.GetOrdersAsync(order.OrderNumber);
            var customers = _orderRepo.GetCustomersAsync(customer.FirstName);


        }
        catch (Exception ex)
        {
            _logger.LogError(" Exception {ex}", ex.ToString());
        }

    }
}

```

## MongoDb original driver

[MongoDB C# Driver](https://www.mongodb.com/docs/drivers/csharp/current/)
- [Connection Options]()



