using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        // Method return Task to be async methods.
        // Void Methods turn in Task methods

        // Two methods to display data using Get Http verb
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        // To enter new data using post Http verb
        Task CreateItemAsync(Item item);
        // Asyn -- stand for asynchronous method
        Task UpdateItemAsync(Item item);
       
        Task DeleteItemAsync(Guid id);

    }
}