using DotNet.Mps.MongoDb.Abstractions;
using DotNet.Mps.MongoDb.DataAccess;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Mps.MongoDb.DataAccess;

/// <summary>
/// A class to insert MongoDb document.
/// </summary>
public partial class MongoDbDataAccess : MongoDbDataAccessBase, IMongoDbDataAccess
{
    /// <summary>
    /// The construct of the MongoDbDataAccess class.
    /// </summary>
    /// <param name="mongoDbContext">A <see cref="IMongoDbContext"/> instance.</param>
    public MongoDbDataAccess(IMongoDbContext mongoDbContext) : base(mongoDbContext)
    {
    }

    #region Create TKey

    /// <summary>
    /// Asynchronously adds a document to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to add.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    public virtual async Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument

    {
        FormatDocument<TDocument>(document);
        await HandlePartitioned<TDocument>(document).InsertOneAsync(document, null, cancellationToken);
    }

    /// <summary>
    /// Adds a document to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to add.</param>
    public virtual void AddOne<TDocument>(TDocument document)
        where TDocument : IStructuredDocument

    {
        FormatDocument<TDocument>(document);
        HandlePartitioned<TDocument>(document).InsertOne(document);
    }

    /// <summary>
    /// Asynchronously adds a list of documents to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The documents you want to add.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument

    {
        if (!documents.Any())
        {
            return;
        }
        foreach (var document in documents)
        {
            FormatDocument<TDocument>(document);
        }
        // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
        if (documents.Any(e => e is IPartitionedDocument))
        {
            foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
            {
                await HandlePartitioned<TDocument>(group.FirstOrDefault()).InsertManyAsync(group.ToList(), null, cancellationToken);
            }
        }
        else
        {
            await GetCollection<TDocument>().InsertManyAsync(documents.ToList(), null, cancellationToken);
        }
    }

    /// <summary>
    /// Adds a list of documents to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The documents you want to add.</param>
    public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents)
        where TDocument : IStructuredDocument

    {
        if (!documents.Any())
        {
            return;
        }
        foreach (var document in documents)
        {
            FormatDocument<TDocument>(document);
        }
        // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
        if (documents.Any(e => e is IPartitionedDocument))
        {
            foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
            {
                HandlePartitioned<TDocument>(group.FirstOrDefault()).InsertMany(group.ToList());
            }
        }
        else
        {
            GetCollection<TDocument>().InsertMany(documents.ToList());
        }
    }

    #endregion

    /// <summary>
    /// Sets the value of the document Id if it is not set already.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="document">The document.</param>
    protected void FormatDocument<TDocument>(TDocument document)
        where TDocument : IStructuredDocument

    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        if (document.Id == Guid.Empty)
        {
            document.Id = IdGenerator.GetId<Guid>();
        }
    }
}
