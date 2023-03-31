using Catalog.Entities;
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
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
