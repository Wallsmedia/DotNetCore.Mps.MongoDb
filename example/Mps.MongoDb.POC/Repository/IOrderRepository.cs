using Mps.MongoDb.POC.Models;

namespace Mps.MongoDb.POC.Repository;

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