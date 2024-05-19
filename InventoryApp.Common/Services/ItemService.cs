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

        var itemModels = _itemMapper.ItemCollectionToItemModelCollection(items);

        return itemModels;
    }

    /// <inheritdoc/>
    public async Task<ItemModel> InsertAsync(ItemModel itemModel)
    {
        var item = _itemMapper.ItemModelToItem(itemModel);
        item.Id = Guid.NewGuid();

        await _context.AddAsync(item);
        await _context.SaveChangesAsync();

        var insertedItem = await _context.FindAsync<Item>(item.Id);
        var returnItem = _itemMapper.ItemToItemModel(insertedItem!);

        return returnItem;
    }
}
