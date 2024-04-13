using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DotNet.Mps.MongoDb.Abstractions;

/// <summary>
/// This class represents a basic document that can be stored in MongoDb.
/// Your document must implement this class in order for the MongoDbRepository to handle them.
/// Use [MongoDbCollectionAttribute] to map for a collection
/// </summary>

[BsonIgnoreExtraElements]
public class StructuredDocument : IStructuredDocument
{
    /// <summary>
    /// The document constructor
    /// </summary>
    public StructuredDocument()
    {
        Id = Guid.NewGuid();
        AddedAtUtc = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// The Id of the document
    /// </summary>
    [BsonId]
    public Guid Id { get; set; }

    /// <summary>
    /// The datetime in UTC at which the document was added.
    /// </summary>
    public DateTimeOffset AddedAtUtc { get; set; }

    /// <summary>
    /// The version of the schema of the document
    /// </summary>
    public int Version { get; set; }
}