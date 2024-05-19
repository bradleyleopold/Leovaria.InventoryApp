using InventoryApp.Common.Models;
using InventoryApp.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace InventoryApp.Common.Mappers;

[Mapper]
public partial class ItemMapper
{
    public partial ItemModel ItemToItemModel(Item item);
    public partial Item ItemModelToItem(ItemModel itemModel);
    public partial List<ItemModel> ItemCollectionToItemModelCollection(IEnumerable<Item> items);
}
