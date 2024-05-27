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
        entity!.Name = item.Name;
        entity!.Description = item.Description;
        entity!.Quantity = item.Quantity;

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
            ArgumentNullException.ThrowIfNull(entity);
        }

        _context.Items.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }
}
