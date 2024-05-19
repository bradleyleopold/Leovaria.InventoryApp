using InventoryApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryApp.Data.Configurations;

/// <summary>
/// Configuration for <see cref="Item"/> entity.
/// </summary>
internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("dbo.Items");

        builder.HasKey(x => x.Id);
    }
}