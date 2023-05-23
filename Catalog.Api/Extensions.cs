using Cartalog.Api.Dtos;
using Cartalog.Api.Entities;

namespace Cartalog.Api
{
    public static class Extensions
    {
        //Calling class ItemDto (copy of Entities/Item)
        public static ItemDto AsDto (this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreateDate = item.CreateDate
            };
        }
    }
}
