using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {

        // Db name
        private const string databaseName = "catalog";

        // Collection name
        private const string collectionName = "items";


        // Declaring a collection // Item is the type of entities
        private readonly IMongoCollection<Item> itemsCollection;

        // FilterDefinitionBuilder -- To filter or return an specific item
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;


        // Construrtor to inject MongoDb (instance of MongoDb client)
        public MongoDbItemsRepository(IMongoClient mongoClient ) 
        {
            // Reference of the Db
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            
            // Reference of the collection
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            // Build the filter
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault(); 
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}
