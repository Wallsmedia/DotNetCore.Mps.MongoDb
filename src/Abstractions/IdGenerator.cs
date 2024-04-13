using MongoDB.Bson;
using System;

namespace DotNet.Mps.MongoDb.Abstractions;

/// <summary>
/// The IdGenerator instance, used to generate Ids of different types.
/// </summary>
public static class IdGenerator
{
    private static readonly Random Random = new Random();

    /// <summary>
    /// Generates a random value of a given type.
    /// </summary>
    /// <typeparam name="TKey">The type of the value to generate.</typeparam>
    /// <returns>A value of type TKey.</returns>
    public static TKey GetId<TKey>()
    {
        var idTypeName = typeof(TKey).Name;
        switch (idTypeName)
        {
            case "Guid":
                return (TKey)(object)Guid.NewGuid();
            case "String":
                return (TKey)(object)Guid.NewGuid().ToString();
            case "ObjectId":
                return (TKey)(object)ObjectId.GenerateNewId();
        }
        throw new ArgumentException($"{idTypeName} is not a supported Id type, the Id of the document cannot be set.");
    }
}
