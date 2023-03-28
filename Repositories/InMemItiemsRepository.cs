using Catalog.Entities;

namespace Catalog.Repositories;


public class InMemItiemsRepository : IItemsRepository
{


    //List of Item
    List<Item> itemsList = new List<Item>()
    {
        new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreateDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreateDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreateDate = DateTimeOffset.UtcNow }
    };

    // Retrive all the items
    public IEnumerable<Item> GetItems()
    {
        return itemsList;
    }
    // Retrive an specifict item
    public Item GetItem(Guid id)
    {
        if (string.IsNullOrEmpty(itemsList.ToString()))
        {
            Console.WriteLine("The Item is null");
        }

        return itemsList.Where(item => item.Id == id).SingleOrDefault();
    }
    // Enter new data or item
    public void CreateItem(Item newItem)
    {
        itemsList.Add(newItem);
    }

    public void UpdateItem(Item item)
    {
        // finding the index of the item it is going to be updated
        var index = itemsList.FindIndex(existingItem => existingItem.Id == item.Id);
        itemsList[index] = item;
    }

    public void DeleteItem(Guid id) 
    {
        var index = itemsList.FindIndex(existingItem => existingItem.Id == id);
        
        itemsList.RemoveAt(index);
    }
}
