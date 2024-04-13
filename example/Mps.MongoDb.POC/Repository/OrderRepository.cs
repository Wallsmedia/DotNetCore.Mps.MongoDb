using Mps.MongoDb.POC.DbContext;
using Mps.MongoDb.POC.Models;

namespace Mps.MongoDb.POC.Repository;

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
