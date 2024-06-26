﻿using DotNet.Mps.MongoDb.Abstractions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mps.MongoDb.DataAccess;

/// <summary>
/// A class for handling  the Mongo Database and its Collections.
/// </summary>
public partial class MongoDbDataAccess 
{
    /// <summary>
    /// Asynchronously returns a projected document matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="projection">The projection expression.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection>(
        Expression<Func<TDocument, bool>> filter, 
        Expression<Func<TDocument, TProjection>> projection, 
        string partitionKey = null,
        CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument
        where TProjection : class
    {
        return await HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                                     .Project(projection)
                                                                     .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Returns a projected document matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="projection">The projection expression.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    public virtual TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
        where TDocument : IStructuredDocument
        where TProjection : class
    {
        return HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                               .Project(projection)
                                                               .FirstOrDefault();
    }

    /// <summary>
    /// Asynchronously returns a list of projected documents matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="projection">The projection expression.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(
        Expression<Func<TDocument, bool>> filter, 
        Expression<Func<TDocument, TProjection>> projection, 
        string partitionKey = null,
        CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument
        where TProjection : class
    {
        return await HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                               .Project(projection)
                                                               .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously returns a list of projected documents matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="projection">The projection expression.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    public virtual List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
        where TDocument : IStructuredDocument
        where TProjection : class
    {
        return HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                               .Project(projection)
                                                               .ToList();
    }
}
