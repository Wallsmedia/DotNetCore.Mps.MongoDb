using DotNet.Mps.MongoDb.Abstractions;
using DotNet.Mps.MongoDb.Attributes;

namespace Mps.MongoDb.POC.Models;

[MongoDbCollection("orders")]
public class OrderStructured : StructuredDocument
{
    public int OrderNumber { get; set; }
    public Address ShippingAddress { get; set; }
    public List<Product> Products { get; set; }
    public OptionalOrderInfo OptionalInfo { get; set; }
}


