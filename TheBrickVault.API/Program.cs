using TheBrickVault.API;
using Microsoft.EntityFrameworkCore;
using TheBrickVault.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LegoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient("RebrickableClient", client =>
{
    client.BaseAddress = new Uri("https://rebrickable.com/api/v3/");
    var apiKey = builder.Configuration["Rebrickable:ApiKey"];
    client.DefaultRequestHeaders.Add("Authorization", $"key {apiKey}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var config = builder.Configuration;
builder.Services.Configure<RebrickableSettings>(config.GetSection("Rebrickable"));

app.UseHttpsRedirection();



app.Run();

