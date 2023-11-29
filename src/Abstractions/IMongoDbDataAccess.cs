using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.Mps.MongoDb.Abstractions
{
    public interface IMongoDbDataAccess
    {
        void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IStructuredDocument;
        Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        void AddOne<TDocument>(TDocument document) where TDocument : IStructuredDocument;
        Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IStructuredDocument;
        long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        long DeleteMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IStructuredDocument;
        Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IStructuredDocument;
        long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        long DeleteOne<TDocument>(TDocument document) where TDocument : IStructuredDocument;
        Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<long> DeleteOneAsync<TDocument>(TDocument document) where TDocument : IStructuredDocument;
        Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null) where TDocument : IStructuredDocument;
        List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        TDocument GetById<TDocument>(Guid id, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<TDocument> GetByIdAsync<TDocument>(Guid id, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey = null) where TDocument : IStructuredDocument;
        TValue GetMaxValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        TValue GetMinValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> sortSelector, bool ascending = true, int skipNumber = 0, int takeNumber = 50, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sortDefinition, int skipNumber = 0, int takeNumber = 50, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> selector, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection, string partitionKey = null)
            where TDocument : IStructuredDocument
            where TProjection : class, new();
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey = null)
            where TDocument : IStructuredDocument
            where TProjection : class, new();
        Task<List<TProjection>> GroupByAsync<TDocument, TGroupKey, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> selector, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IStructuredDocument
            where TProjection : class, new();
        List<TProjection> ProjectMany<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IStructuredDocument
            where TProjection : class;
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IStructuredDocument
            where TProjection : class;
        TProjection ProjectOne<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IStructuredDocument
            where TProjection : class;
        Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IStructuredDocument
            where TProjection : class;
        decimal SumBy<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null) where TDocument : IStructuredDocument;
        int SumBy<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        long UpdateMany<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        long UpdateMany<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> UpdateDefinition, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<long> UpdateManyAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<long> UpdateManyAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<long> UpdateManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<long> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument>(TDocument modifiedDocument) where TDocument : IStructuredDocument;
        bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument) where TDocument : IStructuredDocument;
        Task<bool> UpdateOneAsync<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update) where TDocument : IStructuredDocument;
    }
}