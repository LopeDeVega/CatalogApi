using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cartalog.Api.Entities;

namespace Cartalog.Api.Repositories;


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
    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await Task.FromResult(itemsList);
    }
    // Retrive an specifict item
    public async Task<Item> GetItemAsync(Guid id)
    {
        if (string.IsNullOrEmpty(itemsList.ToString()))
        {
            Console.WriteLine("The Item is null");
        }

        var item = itemsList.Where(item => item.Id == id).SingleOrDefault();
        return await Task.FromResult(item);
    }
    // Enter new data or item
    public async Task CreateItemAsync(Item newItem)
    {
        itemsList.Add(newItem);

        // Because there is nothing to return then we create a Task and return that when it's completed
        await Task.CompletedTask;
    }

    public async Task UpdateItemAsync(Item item)
    {
        // finding the index of the item it is going to be updated
        var index = itemsList.FindIndex(existingItem => existingItem.Id == item.Id);
        itemsList[index] = item;

        // Because there is nothing to return then we create a Task and return that when it's completed
        await Task.CompletedTask;

    }

    public async Task DeleteItemAsync(Guid id) 
    {
        var index = itemsList.FindIndex(existingItem => existingItem.Id == id);
        itemsList.RemoveAt(index);
        
        // Because there is nothing to return then we create a Task and return that when it's completed
        await Task.CompletedTask;
    }
}
