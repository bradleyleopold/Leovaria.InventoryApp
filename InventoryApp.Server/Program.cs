using InventoryApp.Common.Interfaces;
using InventoryApp.Common.Services;
using InventoryApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inventory App Api",
        Description = "Api is used for performing necessary operations in order for the " +
            "InventoryApp Angular UI application to work."
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", x =>
    {
        x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "Inventory App Api - Swagger";
    // Removes the "Schema" section from the Swagger page.
    options.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.UseCors("CORS");

app.Run();
