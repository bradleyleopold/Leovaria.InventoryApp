using InventoryApp.Common.Interfaces;
using InventoryApp.Common.Services;
using InventoryApp.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<InventoryAppDbContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("InventoryAppDb"),
        x => x.MigrationsAssembly("InventoryApp.Data"));
});

builder.Services.AddTransient<IItemService, ItemService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Removes the "Schema" section from the Swagger page.
    options.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
