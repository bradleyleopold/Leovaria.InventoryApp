using InventoryApp.Common.Models;
using InventoryApp.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace InventoryApp.Common.Mappers;

/// <summary>
/// Mappings for <see cref="ItemModel"/> and <see cref="Item"/>.
/// </summary>
[Mapper]
public partial class ItemMapper
{
    /// <summary>
    /// From <see cref="Item"/> to <see cref="ItemModel"/>.
    /// </summary>
    public partial ItemModel Map(Item item);

    /// <summary>
    /// From <see cref="ItemModel"/> to <see cref="Item"/>.
    /// </summary>
    public partial Item Map(ItemModel itemModel);

    /// <summary>
    /// From <see cref="IEnumerable{T}"/> of <see cref="Item"/> to <see cref="List{T}"/>
    /// of <see cref="ItemModel"/>.
    /// </summary>
    public partial List<ItemModel> Map(IEnumerable<Item> items);
}
