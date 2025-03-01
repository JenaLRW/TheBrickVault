
using Microsoft.EntityFrameworkCore;
using TheBrickVault.Infrastructure.Data;
using System.Net.Http;
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

            builder.Services.AddScoped(sp =>
            new HttpClient 
            { 
                BaseAddress = new Uri(builder.Configuration["https://rebrickable.com/api/v3/"] ?? "https://localhost:5002")
            });
                       
            builder.Services.AddHttpClient("RebrickableClient", client =>
            {
                client.BaseAddress = new Uri("https://rebrickable.com/api/v3/");
                var apiKey = builder.Configuration["Rebrickable:ApiKey"];
                client.DefaultRequestHeaders.Add("Authorization", $"key {apiKey}");
            });

            
            // Call the API
            builder.Services.AddSingleton<RebrickableSettings>(provider =>
            {
                string apiKey = builder.Configuration.GetValue<string>("Rebrickable:ApiKey");
                var rebrickableSettings = new RebrickableSettings
                {
                    ApiKey = apiKey
                };
                return rebrickableSettings;
            });

            builder.Services.AddSignalR();

            builder.Services.AddServerSideBlazor();

            var app = builder.Build();


            //HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();

        }
    }
}