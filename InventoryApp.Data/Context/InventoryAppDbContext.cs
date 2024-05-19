using InventoryApp.Data.Configurations;
using InventoryApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Data.Context;

/// <summary>
/// DbContext for InventoryAppDatabase.db.
/// </summary>
public sealed class InventoryAppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public InventoryAppDbContext() { }

    /// <summary>
    /// Options constructor.
    /// </summary>
    public InventoryAppDbContext(DbContextOptions<InventoryAppDbContext> options) : base(options) { }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurations.
        modelBuilder.ApplyConfiguration(new ItemConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
