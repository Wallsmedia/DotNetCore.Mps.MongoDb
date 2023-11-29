using System;

namespace DotNet.Mps.MongoDb.Attributes
{
    /// <summary>
    /// This attribute allows you to specify of the name of the collection.
    /// who has included the CollectionName attribute into the repo to give another choice to the user on how 
    /// to name their collections. 
    /// </summary>
	[AttributeUsage(AttributeTargets.Class)]
    public class MongoDbCollectionAttribute : Attribute
    {
        /// <summary>
        /// The name of the collection in which your documents are stored.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
		public MongoDbCollectionAttribute(string name)
        {
            Name = name;
        }
    }
}
