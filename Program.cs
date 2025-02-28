
using Microsoft.EntityFrameworkCore;
using TheBrickVault.Infrastructure.Data;

using System;
using Microsoft.Extensions.DependencyInjection;
using TheBrickVault.Services;


namespace TheBrickVault
{

    class Program
    {

        // Main Method
        public static void Main(String[] args)
        {

            Console.WriteLine("Main Method");


            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<LegoDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            //builder.Services.AddHttpClient("RebrickableClient", client =>
            //{
            //    client.BaseAddress = new Uri("https://rebrickable.com/api/v3/");
            //    var apiKey = builder.Configuration["Rebrickable:ApiKey"];
            //    client.DefaultRequestHeaders.Add("Authorization", $"key {apiKey}");
            //},
            
            builder.Services.AddSingleton<RebrickableSettings>(provider =>
            {
                string apiKey = builder.Configuration.GetValue<string>("Rebrickable:ApiKey");
                var rebrickableSettings = new RebrickableSettings
                {
                    ApiKey = apiKey
                };
                return rebrickableSettings;
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            
            
            //var config = builder.Configuration;
            //builder.Services.Configure<RebrickableSettings>(config.GetSection("Rebrickable"));

            app.UseHttpsRedirection();



            app.Run();

        }
    }
}