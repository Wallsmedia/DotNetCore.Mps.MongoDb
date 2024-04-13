using DotNet.Mps.MongoDb.Abstractions;
using DotNet.Mps.MongoDb.Attributes;

namespace Mps.MongoDb.POC.Models;


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


