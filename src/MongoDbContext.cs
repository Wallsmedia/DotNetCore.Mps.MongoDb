﻿using DotNet.Mps.MongoDb.Abstractions;
using DotNet.Mps.MongoDb.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Reflection;

namespace DotNet.Mps.MongoDb;

/// <summary>
/// The MongoDb context
/// </summary>
public class MongoDbContext : IMongoDbContext
{
    /// <summary>
    /// The IMongoClient from the official MongoDB driver
    /// </summary>
    public IMongoClient Client { get; }

    /// <summary>
    /// The IMongoDatabase from the official MongoDB driver
    /// </summary>
    public IMongoDatabase Database { get; }


    /// <summary>
    /// The constructor of the MongoDbContext, it needs an object implementing <see cref="IMongoDatabase"/>.
    /// </summary>
    /// <param name="mongoDatabase">An object implementing IMongoDatabase</param>
    public MongoDbContext(IMongoDatabase mongoDatabase)
    {
        // Avoid legacy UUID representation: use Binary 0x04 subtype.
        InitializeGuidRepresentation();
        Database = mongoDatabase;
        Client = Database.Client;
    }

    /// <summary>
    /// The constructor of the MongoDbContext, it needs a connection string and a database name. 
    /// </summary>
    /// <param name="connectionString">The connections string.</param>
    /// <param name="databaseName">The name of your database.</param>
    public MongoDbContext(string connectionString, string databaseName)
    {
        InitializeGuidRepresentation();
        Client = new MongoClient(connectionString);
        Database = Client.GetDatabase(databaseName);
    }

    /// <summary>
    /// Initialise an instance of a <see cref="IMongoDbContext"/> using a connection string
    /// </summary>
    /// <param name="connectionString"></param>
    public MongoDbContext(string connectionString)
        : this(connectionString, new MongoUrl(connectionString).DatabaseName)
    {
    }

    /// <summary>
    /// The constructor of the MongoDbContext, it needs a connection string and a database name. 
    /// </summary>
    /// <param name="client">The MongoClient.</param>
    /// <param name="databaseName">The name of your database.</param>
    public MongoDbContext(MongoClient client, string databaseName)
    {
        InitializeGuidRepresentation();
        Client = client;
        Database = client.GetDatabase(databaseName);
    }

    /// <summary>
    /// Returns a collection for a document type. Also handles document types with a partition key.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="partitionKey">The optional value of the partition key.</param>
    public virtual IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null)
    {
        return Database.GetCollection<TDocument>(GetCollectionName<TDocument>(partitionKey));
    }

    /// <summary>
    /// Drops a collection, use very carefully.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    public virtual void DropCollection<TDocument>(string partitionKey = null)
    {
        Database.DropCollection(GetCollectionName<TDocument>(partitionKey));
    }

    /// <summary>
    /// Sets the Guid representation of the MongoDB Driver.
    /// </summary>
    /// <param name="guidRepresentation">The new value of the GuidRepresentation</param>
    public virtual void SetGuidRepresentation(MongoDB.Bson.GuidRepresentation guidRepresentation)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(guidRepresentation));
    }

    /// <summary>
    /// Extracts the CollectionName attribute from the entity type, if any.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <returns>The name of the collection in which the TDocument is stored.</returns>
    protected virtual string GetAttributeCollectionName<TDocument>()
    {
        return (typeof(TDocument).GetTypeInfo()
                                 .GetCustomAttributes(typeof(MongoDbCollectionAttribute))
                                 .FirstOrDefault() as MongoDbCollectionAttribute)?.Name;
    }

    /// <summary>
    /// Initialize the Guid representation of the MongoDB Driver.
    /// Override this method to change the default GuidRepresentation.
    /// </summary>
    protected virtual void InitializeGuidRepresentation()
    {
        // by default, avoid legacy UUID representation: use Binary 0x04 subtype.
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }

    /// <summary>
    /// Given the document type and the partition key, returns the name of the collection it belongs to.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="partitionKey">The value of the partition key.</param>
    /// <returns>The name of the collection.</returns>
    protected virtual string GetCollectionName<TDocument>(string partitionKey)
    {
        var collectionName = GetAttributeCollectionName<TDocument>() ?? typeof(TDocument).Name;
        if (string.IsNullOrEmpty(partitionKey))
        {
            return collectionName;
        }
        return $"{partitionKey}-{collectionName}";
    }

}
