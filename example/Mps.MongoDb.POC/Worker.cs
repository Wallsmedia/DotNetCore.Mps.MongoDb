using AutoFixture;
using Mps.MongoDb.POC.Models;
using Mps.MongoDb.POC.Repository;

namespace Mps.MongoDb.POC;

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
