using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.Mps.MongoDb.Abstractions;

/// <summary>
/// A interface for handling  the Mongo Database and its Collections.
/// </summary>
public interface IMongoDbDataAccess
{
    /// <summary>
    /// Asynchronously adds a document to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to add.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;

    /// <summary>
    /// Adds a document to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to add.</param>
    void AddOne<TDocument>(TDocument document) where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously adds a list of documents to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The documents you want to add.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Adds a list of documents to the collection.
    /// Populates the Id and AddedAtUtc fields if necessary.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The documents you want to add.</param>
    void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets a IMongoQueryable for a potentially partitioned document type and a filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="filter">The filter definition.</param>
    /// <param name="partitionKey">The collection partition key.</param>
    /// <returns></returns>
    IMongoQueryable<TDocument> GetQuery<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets a collections for a potentially partitioned document type.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="document">The document.</param>
    /// <returns></returns>
    IMongoCollection<TDocument> HandlePartitioned<TDocument>(TDocument document) where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets a collections for the type TDocument with a partition key.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="partitionKey">The collection partition key.</param>
    /// <returns></returns>
    IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null) where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets a collections for a potentially partitioned document type.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="partitionKey">The collection partition key.</param>
    /// <returns></returns>
    IMongoCollection<TDocument> HandlePartitioned<TDocument>(string partitionKey) where TDocument : IStructuredDocument;


    /// <summary>
    /// Deletes a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    long DeleteOne<TDocument>(TDocument document) where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="document">The document you want to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    Task<long> DeleteOneAsync<TDocument>(TDocument document) where TDocument : IStructuredDocument;

    /// <summary>
    /// Deletes a document matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously deletes the documents matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously deletes a list of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The list of documents to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IStructuredDocument;

    /// <summary>
    /// Deletes a list of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documents">The list of documents to delete.</param>
    /// <returns>The number of documents deleted.</returns>
    long DeleteMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IStructuredDocument;

    /// <summary>
    /// Deletes the documents matching the condition of the LINQ expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The number of documents deleted.</returns>
    long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;

    /// <summary>
    /// Returns the names of the indexes present on a collection.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="partitionKey">An optional partition key</param>
    /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
    Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey = null) where TDocument : IStructuredDocument;

    /// <summary>
    /// Create a text index on the given field.
    /// IndexCreationOptions can be supplied to further specify 
    /// how the creation should be done.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="field">The field we want to index.</param>
    /// <param name="indexCreationOptions">Options for creating an index.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The result of the create index operation.</returns>
    Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Creates an index on the given field in ascending order.
    /// IndexCreationOptions can be supplied to further specify 
    /// how the creation should be done.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="field">The field we want to index.</param>
    /// <param name="indexCreationOptions">Options for creating an index.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The result of the create index operation.</returns>
    Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Creates an index on the given field in descending order.
    /// IndexCreationOptions can be supplied to further specify 
    /// how the creation should be done.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="field">The field we want to index.</param>
    /// <param name="indexCreationOptions">Options for creating an index.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The result of the create index operation.</returns>
    Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Creates a hashed index on the given field.
    /// IndexCreationOptions can be supplied to further specify 
    /// how the creation should be done.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="field">The field we want to index.</param>
    /// <param name="indexCreationOptions">Options for creating an index.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The result of the create index operation.</returns>
    Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Creates a combined text index.
    /// IndexCreationOptions can be supplied to further specify 
    /// how the creation should be done.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="fields">The fields we want to index.</param>
    /// <param name="indexCreationOptions">Options for creating an index.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <returns>The result of the create index operation.</returns>
    Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Drops the index given a field name
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="indexName">The name of the index</param>
    /// <param name="partitionKey">An optional partition key</param>
    Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null) where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously returns one document given its id.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="id">The Id of the document you want to get.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<TDocument> GetByIdAsync<TDocument>(Guid id, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Returns one document given its id.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="id">The Id of the document you want to get.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    TDocument GetById<TDocument>(Guid id, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously returns one document given an expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Returns one document given an expression filter.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Returns a collection cursor.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Returns true if any of the document of the collection matches the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Returns true if any of the document of the collection matches the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously returns a list of the documents matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Returns a list of the documents matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously counts how many documents match the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partitionKey</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Counts how many documents match the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="partitionKey">An optional partitionKey</param>
    long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="maxValueSelector">A property selector to order by descending.</param>
    /// <param name="partitionKey">An optional partitionKey.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="maxValueSelector">A property selector to order by descending.</param>
    /// <param name="partitionKey">An optional partitionKey.</param>
    TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the document with the minimum value of a specified property in a MongoDB collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="minValueSelector">A property selector to order by ascending.</param>
    /// <param name="partitionKey">An optional partitionKey.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the document with the minimum value of a specified property in a MongoDB collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="minValueSelector">A property selector to order by ascending.</param>
    /// <param name="partitionKey">An optional partitionKey.</param>
    TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <typeparam name="TValue">The type of the field for which you want the maximum value.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="maxValueSelector">A property selector to order by ascending.</param>
    /// <param name="partitionKey">An optional partitionKey.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="maxValueSelector">A property selector to order by ascending.</param>
    /// <param name="partitionKey">An optional partitionKey.</param>
    TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="minValueSelector">A property selector to order by ascending.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null, CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
    /// </summary>
    /// <typeparam name="TDocument">The document type.</typeparam>
    /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="minValueSelector">A property selector to order by ascending.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Sums the values of a selected field for a given filtered collection of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="selector">The field you want to sum.</param>
    /// <param name="partitionKey">The partition key of your document, if any.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter,
                                                   Expression<Func<TDocument, int>> selector,
                                                   string partitionKey = null,
                                                   CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Sums the values of a selected field for a given filtered collection of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="selector">The field you want to sum.</param>
    /// <param name="partitionKey">The partition key of your document, if any.</param>
    int SumBy<TDocument>(Expression<Func<TDocument, bool>> filter,
                                                   Expression<Func<TDocument, int>> selector,
                                                   string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Sums the values of a selected field for a given filtered collection of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="selector">The field you want to sum.</param>
    /// <param name="partitionKey">The partition key of your document, if any.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter,
                                                   Expression<Func<TDocument, decimal>> selector,
                                                   string partitionKey = null, CancellationToken cancellationToken = default)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Sums the values of a selected field for a given filtered collection of documents.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="selector">The field you want to sum.</param>
    /// <param name="partitionKey">The partition key of your document, if any.</param>
    decimal SumBy<TDocument>(Expression<Func<TDocument, bool>> filter,
                                                   Expression<Func<TDocument, decimal>> selector,
                                                   string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Groups a collection of documents given a grouping criteria, 
    /// and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
    /// <typeparam name="TProjection">The type of the projected group.</typeparam>
    /// <param name="groupingCriteria">The grouping criteria.</param>
    /// <param name="groupProjection">The projected group result.</param>
    /// <param name="partitionKey">The partition key of your document, if any.</param>
    List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
        Expression<Func<TDocument, TGroupKey>> groupingCriteria,
        Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
        string partitionKey = null)
        where TDocument : IStructuredDocument
        where TProjection : class, new();

    /// <summary>
    /// Groups filtered a collection of documents given a grouping criteria, 
    /// and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
    /// <typeparam name="TProjection">The type of the projected group.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="selector">The grouping criteria.</param>
    /// <param name="projection">The projected group result.</param>
    /// <param name="partitionKey">The partition key of your document, if any.</param>
    List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
        Expression<Func<TDocument, bool>> filter,
        Expression<Func<TDocument, TGroupKey>> selector,
        Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection,
        string partitionKey = null)
            where TDocument : IStructuredDocument
            where TProjection : class, new();

    /// <summary>
    /// Groups filtered a collection of documents given a grouping criteria, 
    /// and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
    /// <typeparam name="TProjection">The type of the projected group.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="selector">The grouping criteria.</param>
    /// <param name="projection">The projected group result.</param>
    /// <param name="partitionKey">The partition key of your document, if any.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<List<TProjection>> GroupByAsync<TDocument, TGroupKey, TProjection>(
       Expression<Func<TDocument, bool>> filter,
       Expression<Func<TDocument, TGroupKey>> selector,
       Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection,
       string partitionKey = null,
       CancellationToken cancellationToken = default)
           where TDocument : IStructuredDocument
           where TProjection : class, new();


    /// <summary>
    /// Asynchronously returns a paginated list of the documents matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="sortSelector">The property selector.</param>
    /// <param name="ascending">Order of the sorting.</param>
    /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
    /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
       Expression<Func<TDocument, bool>> filter,
       Expression<Func<TDocument, object>> sortSelector,
       bool ascending = true,
       int skipNumber = 0,
       int takeNumber = 50,
       string partitionKey = null,
       CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously returns a paginated list of the documents matching the filter condition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">A LINQ expression filter.</param>
    /// <param name="sortDefinition">The sort definition.</param>
    /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
    /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
    /// <param name="partitionKey">An optional partition key.</param>
    /// <param name="cancellationToken">An optional cancellation Token.</param>
    Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
       Expression<Func<TDocument, bool>> filter,
       SortDefinition<TDocument> sortDefinition,
       int skipNumber = 0,
       int takeNumber = 50,
       string partitionKey = null,
       CancellationToken cancellationToken = default)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    Task<bool> UpdateOneAsync<TDocument>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    bool UpdateOne<TDocument>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="documentToModify">The document to modify.</param>
    /// <param name="update">The update definition.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    Task<bool> UpdateOneAsync<TDocument>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="documentToModify">The document to modify.</param>
    /// <param name="update">The update definition.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    bool UpdateOne<TDocument>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field to update.</typeparam>
    /// <param name="session">The client session.</param> 
    /// <param name="documentToModify">The document to modify.</param>
    /// <param name="field">The field to update.</param>
    /// <param name="value">The value of the field.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    Task<bool> UpdateOneAsync<TDocument, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field to update.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="documentToModify">The document to modify.</param>
    /// <param name="field">The field to update.</param>
    /// <param name="value">The value of the field.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    bool UpdateOne<TDocument, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field to update.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="filter">The filter for the update.</param>
    /// <param name="field">The field to update.</param>
    /// <param name="value">The value of the field.</param>
    /// <param name="partitionKey">The optional partition key.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    Task<bool> UpdateOneAsync<TDocument, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field to update.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="filter">The filter for the update.</param>
    /// <param name="field">The field to update.</param>
    /// <param name="value">The value of the field.</param>
    /// <param name="partitionKey">The optional partition key.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    Task<bool> UpdateOneAsync<TDocument, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field to update.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="filter">The filter for the update.</param>
    /// <param name="field">The field to update.</param>
    /// <param name="value">The value of the field.</param>
    /// <param name="partitionKey">The optional partition key.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    bool UpdateOne<TDocument, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field to update.</typeparam>
    /// <param name="session">The client session.</param>
    /// <param name="filter">The filter for the update.</param>
    /// <param name="field">The field to update.</param>
    /// <param name="value">The value of the field.</param>
    /// <param name="partitionKey">The optional partition key.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns></returns>
    bool UpdateOne<TDocument, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Asynchronously Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
    Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
    bool UpdateOne<TDocument>(TDocument modifiedDocument)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Takes a document you want to modify and applies the update you have defined in MongoDb.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documentToModify">The document you want to modify.</param>
    /// <param name="update">The update definition for the document.</param>
    Task<bool> UpdateOneAsync<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Takes a document you want to modify and applies the update you have defined in MongoDb.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="documentToModify">The document you want to modify.</param>
    /// <param name="update">The update definition for the document.</param>
    bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates the property field with the given value update a property field in entities.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="documentToModify">The document you want to modify.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    Task<bool> UpdateOneAsync<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates the property field with the given value update a property field in entities.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="documentToModify">The document you want to modify.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    bool UpdateOne<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates the property field with the given value update a property field in entities.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The value of the partition key.</param>
    Task<bool> UpdateOneAsync<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entity selected by the filter, updates the property field with the given value.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The partition key for the document.</param>
    Task<bool> UpdateOneAsync<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// Updates the property field with the given value.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The value of the partition key.</param>
    bool UpdateOne<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entity selected by the filter, updates the property field with the given value.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The partition key for the document.</param>
    bool UpdateOne<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entities selected by the filter, updates the property field with the given value.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The partition key for the document.</param>
    Task<long> UpdateManyAsync<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entities selected by the filter, updates the property field with the given value.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The value of the partition key.</param>
    Task<long> UpdateManyAsync<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entities selected by the filter, apply the update definition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="update">The new value.</param>
    /// <param name="partitionKey">The value of the partition key.</param>
    Task<long> UpdateManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entities selected by the filter, apply the update definition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="updateDefinition">The update definition.</param>
    /// <param name="partitionKey">The value of the partition key.</param>
    Task<long> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
       where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entities selected by the filter, updates the property field with the given value.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The partition key for the document.</param>
    long UpdateMany<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entities selected by the filter, updates the property field with the given value.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="field">The field selector.</param>
    /// <param name="value">The new value of the property field.</param>
    /// <param name="partitionKey">The value of the partition key.</param>
    long UpdateMany<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        where TDocument : IStructuredDocument;

    /// <summary>
    /// For the entities selected by the filter, apply the update definition.
    /// </summary>
    /// <typeparam name="TDocument">The type representing a Document.</typeparam>
    /// <param name="filter">The document filter.</param>
    /// <param name="UpdateDefinition">The update definition.</param>
    /// <param name="partitionKey">The value of the partition key.</param>
    long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> UpdateDefinition, string partitionKey = null)
        where TDocument : IStructuredDocument;

}