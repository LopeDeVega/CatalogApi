using Microsoft.AspNetCore.Mvc;
using Cartalog.Api.Repositories;
using Cartalog.Api.Entities;
using Cartalog.Api.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Cartalog.Api.Controllers;


// ControllerBase turn the class into a contraller-class (inheriting all the necessary tools)
[ApiController]
[Route("items")]
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
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var item = (await repository.GetItemsAsync()).Select(item => item.AsDto());
        return item;
    }

    //ActonResult allow to return more than one thing
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
        var item = await  repository.GetItemAsync(id);

        //Return a status code when the item is not found
        if (item == null)
        {
            return NotFound();
        }

        return item.AsDto();
    }

    //Post / items
    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
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
        await repository.CreateItemAsync(newItem);

        return CreatedAtAction(nameof(GetItemAsync), new { id = newItem.Id }, newItem.AsDto());
    }

    //Needed the Guid id for the item and also the new name and price so updateItemDto data
    // Put/items/id
    [HttpPut("{id}")]
    public async Task<ActionResult<ItemDto>> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
    {
        //Get the item to update
        var existingItem = await repository.GetItemAsync(id);

        if (existingItem is null)
        {
            return NotFound(nameof(GetItemAsync));
        }

        //with expression (copy of the item modifing the data needed)
        Item updateitem = existingItem with
        {
            Name = updateItemDto.Name,
            Price = updateItemDto.Price
        };

        await repository.UpdateItemAsync(updateitem);

        return NoContent();
    }

    //Delete / item/ {id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemDto>> DeleteItemAsync(Guid id)
    {
        if(string.IsNullOrEmpty(id.ToString()))
        { 
            return NotFound(); 
        }

        var removeItem = await repository.GetItemAsync(id);

        await repository.DeleteItemAsync(id);

        return CreatedAtAction(nameof(GetItemAsync), new { id = removeItem }, removeItem.AsDto());

    } 



}
