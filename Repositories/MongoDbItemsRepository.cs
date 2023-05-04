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


        // Construrtor to inject MongoDb from Program.cs (instance of MongoDb client)
        public MongoDbItemsRepository(IMongoClient mongoClient) 
        {
            // Reference of the Db
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            
            // Reference of the collection
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        // Turning the methods into asynchronous need --> async before Task and await make a async call 
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, id);
            await itemsCollection.DeleteOneAsync(filter); 
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            // Build the filter
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}
