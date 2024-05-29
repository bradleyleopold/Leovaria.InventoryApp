using InventoryApp.Common.Interfaces;
using InventoryApp.Common.Mappers;
using InventoryApp.Common.Models;
using InventoryApp.Data.Context;
using InventoryApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Common.Services;

/// <inheritdoc cref="IItemService"/>
public sealed class ItemService : IItemService
{
    private readonly InventoryAppDbContext _context;
    private readonly ItemMapper _itemMapper;

    /// <summary>
    /// Dependency injection constructor.
    /// </summary>
    public ItemService(InventoryAppDbContext context)
    {
        _context = context;
        _itemMapper = new ItemMapper();
    }

    /// <inheritdoc/>
    public async Task<List<ItemModel>> GetAllAsync()
    {
        var items = await _context.Items
            .AsNoTracking()
            .ToListAsync();

        var itemModels = _itemMapper.Map(items);

        return itemModels;
    }

    /// <inheritdoc/>
    public async Task<ItemModel> InsertAsync(ItemModel itemModel)
    {
        var item = _itemMapper.Map(itemModel);
        item.Id = Guid.NewGuid();

        await _context.AddAsync(item);
        await _context.SaveChangesAsync();

        var insertedItem = await _context.FindAsync<Item>(item.Id);
        var returnItem = _itemMapper.Map(insertedItem!);

        return returnItem;
    }

    /// <inheritdoc/>
    public async Task<ItemModel> EditAsync(ItemModel itemModel)
    {
        var item = _itemMapper.Map(itemModel);

        var entity = await _context.FindAsync<Item>(item.Id);

        if (entity is null)
        {
            // If we got here, that means whatever ID was sent to
            // us did not yield an item from the database. We can't
            // update something that doesn't exist.
            throw new KeyNotFoundException();
        }

        entity.Name = item.Name;
        entity.Description = item.Description;
        entity.Quantity = item.Quantity;

        await _context.SaveChangesAsync();
        var updatedItem = await _context.FindAsync<Item>(item.Id);
        var returnItem = _itemMapper.Map(updatedItem!);

        return returnItem;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid itemId)
    {
        var entity = await _context.FindAsync<Item>(itemId);

        if (entity is null)
        {
            // If we got here, that means whatever ID was sent to
            // us did not yield an item from the database. Therefore,
            // we return an exception since there's nothing to
            // delete.
            throw new KeyNotFoundException();
        }

        _context.Items.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }
}
