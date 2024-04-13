using DotNet.Mps.MongoDb.Abstractions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mps.MongoDb.DataAccess;

public partial class MongoDbDataAccess  
{
  

    #region Delete TKey

    /// <summary>
    /// Deletes a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual long DeleteOne<TDocument>(TDocument document)
        where TDocument : IStructuredDocument
        
    {
        var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);
        return HandlePartitioned<TDocument>(document).DeleteOne(filter).DeletedCount;
    }

    /// <summary>
    /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual async Task<long> DeleteOneAsync<TDocument>(TDocument document)
        where TDocument : IStructuredDocument
        
    {
        var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);
        return (await HandlePartitioned<TDocument>(document).DeleteOneAsync(filter)).DeletedCount;
    }

    /// <summary>
    /// Deletes a document matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument
        
    {
        return HandlePartitioned<TDocument>(partitionKey).DeleteOne(filter).DeletedCount;
    }

    /// <summary>
    /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument
        
    {
        return (await HandlePartitioned<TDocument>(partitionKey).DeleteOneAsync(filter)).DeletedCount;
    }

    /// <summary>
    /// Asynchronously deletes the documents matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument
        
    {
        return (await HandlePartitioned<TDocument>(partitionKey).DeleteManyAsync(filter)).DeletedCount;
    }

    /// <summary>
    /// Asynchronously deletes a list of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The list of documents to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual async Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents)
        where TDocument : IStructuredDocument
        
    {
        if (!documents.Any())
        {
            return 0;
        }
        // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
        if (documents.Any(e => e is IPartitionedDocument))
        {
            long deleteCount = 0;
            foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
            {
                var groupIdsTodelete = group.Select(e => e.Id).ToArray();
                deleteCount += (await HandlePartitioned<TDocument>(group.FirstOrDefault()).DeleteManyAsync(x => groupIdsTodelete.Contains(x.Id))).DeletedCount;
            }
            return deleteCount;
        }
        else
        {
            var idsTodelete = documents.Select(e => e.Id).ToArray();
            return (await HandlePartitioned<TDocument>(documents.FirstOrDefault()).DeleteManyAsync(x => idsTodelete.Contains(x.Id))).DeletedCount;
        }
    }

    /// <summary>
    /// Deletes a list of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The list of documents to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual long DeleteMany<TDocument>(IEnumerable<TDocument> documents)
        where TDocument : IStructuredDocument
        
    {
        if (!documents.Any())
        {
            return 0;
        }
        // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
        if (documents.Any(e => e is IPartitionedDocument))
        {
            long deleteCount = 0;
            foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
            {
                var groupIdsTodelete = group.Select(e => e.Id).ToArray();
                deleteCount += HandlePartitioned<TDocument>(group.FirstOrDefault()).DeleteMany(x => groupIdsTodelete.Contains(x.Id)).DeletedCount;
            }
            return deleteCount;
        }
        else
        {
            var idsTodelete = documents.Select(e => e.Id).ToArray();
            return HandlePartitioned<TDocument>(documents.FirstOrDefault()).DeleteMany(x => idsTodelete.Contains(x.Id)).DeletedCount;
        }
    }

    /// <summary>
    /// Deletes the documents matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    public virtual long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument
        
    {
        return HandlePartitioned<TDocument>(partitionKey).DeleteMany(filter).DeletedCount;
    }

    #endregion

}
