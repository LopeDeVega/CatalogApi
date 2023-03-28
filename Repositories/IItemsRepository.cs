using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        // Two methods to display data using Get Http verb
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        // To enter new data using post Http verb
        void CreateItem(Item item);

        void UpdateItem(Item item);

        void DeleteItem(Guid id);

    }
}