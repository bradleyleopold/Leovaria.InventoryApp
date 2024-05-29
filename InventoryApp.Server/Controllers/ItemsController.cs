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
    /// <summary>
    /// Service for performing operations for items.
    /// </summary>
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
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ItemModel>>> Get()
    {
        try
        {
            var items = await _itemService.GetAllAsync();
            return Ok(items);
        }
        catch (Exception ex) 
        {
            // Normally you'd log the error, but for this project we will
            // just write a line to the console. At least, for now.
            Console.Write(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
        }
    }

    /// <summary>
    /// Inserts an item into the database.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ActionResult<ItemModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ItemModel>> Post([FromBody] ItemModel item)
    {
        try
        {
            var insertedItem = await _itemService.InsertAsync(item);
            return Ok(insertedItem);
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
        }
    }

    /// <summary>
    /// Modifies an existing item in the database.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ActionResult<ItemModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ItemModel>> Put([FromBody] ItemModel item)
    {
        try
        {
            var updatedItem = await _itemService.EditAsync(item);
            return Ok(updatedItem);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status404NotFound, $"Item with ID {item.Id} not found.");
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
        }
    }

    /// <summary>
    /// Deletes an item from the database based on <paramref name="id"/>.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _itemService.DeleteAsync(id);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status404NotFound, $"Item with ID {id} not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
        }
    }
}
