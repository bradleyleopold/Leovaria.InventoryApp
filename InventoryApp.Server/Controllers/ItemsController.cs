using InventoryApp.Common.Interfaces;
using InventoryApp.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Server.Controllers;

/// <summary>
/// Controller for performing CRUD operations for items.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    /// <summary>
    /// Dependency injection constructor.
    /// </summary>
    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Retrieves all items in the database.
    /// </summary>
    [HttpGet]
    public async Task<List<ItemModel>> Get()
    {
        var items = await _itemService.GetAllAsync();
        return items;
    }

    /// <summary>
    /// Inserts an item into the database.
    /// </summary>
    [HttpPost]
    public async Task<ItemModel> Post([FromBody] ItemModel item)
    {
        var insertedItem = await _itemService.InsertAsync(item);
        return insertedItem;
    }

    /// <summary>
    /// Modifies an existing item in the database.
    /// </summary>
    [HttpPut]
    public async Task<ItemModel> Put([FromBody] ItemModel item)
    {
        var updatedItem = await _itemService.EditAsync(item);
        return updatedItem;
    }

    /// <summary>
    /// Deletes an item from the database based on <paramref name="itemId"/>.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await _itemService.DeleteAsync(id);
    }
}
