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
    [ProducesResponseType(typeof(ActionResult<List<ItemModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ItemModel>>> Get()
    {
        var items = await _itemService.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Inserts an item into the database.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ActionResult<ItemModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ItemModel>> Post([FromBody] ItemModel item)
    {
        var insertedItem = await _itemService.InsertAsync(item);
        return Ok(insertedItem);
    }

    /// <summary>
    /// Modifies an existing item in the database.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ActionResult<ItemModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ItemModel>> Put([FromBody] ItemModel item)
    {
        var updatedItem = await _itemService.EditAsync(item);
        return Ok(updatedItem);
    }

    /// <summary>
    /// Deletes an item from the database based on <paramref name="id"/>.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _itemService.DeleteAsync(id);
        return Ok();
    }
}
