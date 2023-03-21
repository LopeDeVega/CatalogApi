using Catalog.Entities;

namespace Catalog.Repositories;


public class InMemItiemsRepository
{
    //List of Item
    List<Item> items = new List<Item>()
    {
        new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreateDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreateDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreateDate = DateTimeOffset.UtcNow }
    };

    // Get method to retrive an items
    public IEnumerable<Item> GetItems()
    {
        return items;
    }

    public Item GetItem(Guid id)
    {
        if (string.IsNullOrEmpty(items.ToString()))
        {
            Console.WriteLine("The Item is null");
        }

        return items.Where(item => item.Id == id).SingleOrDefault();
    }

}
