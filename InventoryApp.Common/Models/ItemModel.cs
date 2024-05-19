namespace InventoryApp.Common.Models;

/// <summary>
/// An object that will be tracked in the inventory database.
/// </summary>
public sealed class ItemModel
{
    /// <summary>
    /// Primary key ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the item.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the item.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the item that is in stock.
    /// </summary>
    public long Quantity { get; set; }
}
