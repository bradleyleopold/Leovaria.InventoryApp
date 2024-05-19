using InventoryApp.Common.Models;
using InventoryApp.Data.Entities;

namespace InventoryApp.Common.Interfaces;

/// <summary>
/// Service used for CRUD operations with <see cref="ItemModel"/> and <see cref="Item"/>.
/// </summary>
public interface IItemService
{
    /// <summary>
    /// Retrieves all <see cref="Item"/>s from the database.
    /// </summary>
    public Task<List<ItemModel>> GetAllAsync();

    /// <summary>
    /// Inserts <paramref name="itemModel"/> into the database as an <see cref="Item"/>.
    /// </summary>
    public Task<ItemModel> InsertAsync(ItemModel itemModel);
}