using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using Catalog.Dtos;

namespace Catalog.Controllers;


// ControllerBase turn the class into a contraller-class (inheriting all the necessary tools)
[ApiController]
[Route("item")]
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository repository;

    //Contructor
    public ItemsController(IItemsRepository repository)
    {
        this.repository = repository;
    }

    //Http verb // atribute
    [HttpGet]
    // Get a list of items stored stored the InMeItiemsRepository
    public IEnumerable<ItemDto> GetItems()
    {
        var item = repository.GetItems().Select(item => item.AsDto());
        return item;
    }

    //ActonResult allow to return more than one thing
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
        var item = repository.GetItem(id);

        //Return a status code when the item is not found
        if (item == null)
        {
            return NotFound();
        }

        return item.AsDto();
    }

    //Post / items
    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
    {
        // Creating a new itme
        Item newItem = new Item
        {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreateDate = DateTime.Now,
        };

        // Sending the new item to the repository
        repository.CreateItem(newItem);

        return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem.AsDto());
    }

    //Needed the Guid id for the item and also the new name and price so updateItemDto data
    // Put/items/id
    [HttpPut("{id}")]
    public ActionResult<ItemDto> UpdateItem(Guid id, UpdateItemDto updateItemDto)
    {
        //Get the item to update
        var existingItem = repository.GetItem(id);

        if (existingItem is null)
        {
            return NotFound(nameof(GetItem));
        }

        //with expression (copy of the item modifing the data needed)
        Item updateitem = existingItem with
        {
            Name = updateItemDto.Name,
            Price = updateItemDto.Price
        };

        repository.UpdateItem(updateitem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult<ItemDto> DeleteItem(Guid id)
    {
        if(string.IsNullOrEmpty(id.ToString()))
        { 
            return NotFound(); 
        }

        var removeItem = repository.GetItem(id);

        repository.DeleteItem(removeItem);

        return CreatedAtAction(nameof(GetItem), new { id = removeItem }, removeItem.AsDto());
        

    } 



}
