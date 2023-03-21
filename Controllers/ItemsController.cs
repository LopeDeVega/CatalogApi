using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;

namespace Catalog.Controllers;


// ControllerBase turn the class into a contraller-class (inheriting all the necessary tools)
[ApiController]
[Route("item")]
public class ItemsController : ControllerBase
{
    private readonly InMemItiemsRepository repository;

    //Contructor
    public ItemsController()
    {
        repository = new InMemItiemsRepository();
    }

    //Http verb // atribute
    [HttpGet]
    // Get a list of items stored stored the InMeItiemsRepository
    public IEnumerable<Item> GetItems()
    {
        var item = repository.GetItems();
        return item;
    }

    //ActonResult allow to return more than one thing
    [HttpGet("{id}")]
    public ActionResult<Item> GetItem(Guid id)
    {
        var item = repository.GetItem(id);

        //Return a status code when the item is not found
        if(item == null)
        {
            return NotFound();
        }

        return item;
    }



}
