
using Microsoft.EntityFrameworkCore;
using TheBrickVault.Infrastructure.Data;
using System.Net.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using TheBrickVault.Infrastructure;
using TheBrickVault.Components.Services;



namespace TheBrickVault
{

    class Program
    {

        // Main Method
        public static void Main(String[] args)
        {
            Console.WriteLine("Main Method");


            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor(); //Blazor Server application

            //this makes sure the secrets (API key) are loaded before the app starts using configuration values. 
            if (builder.Environment.IsDevelopment()) 
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            //Database file
            builder.Services.AddDbContext<LegoDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            //LegoSetService Registration
            //builder.Services.AddScoped<LegoSetService>();


            // Rebrickable API
            builder.Services.AddScoped(sp =>
            new HttpClient 
            { 
                BaseAddress = new Uri(builder.Configuration["Rebrickable:https://rebrickable.com/api/v3/"] ?? "https://localhost:5002")
            });
            
            
            //HttpClient 
            builder.Services.AddHttpClient("RebrickableClient", client =>
            {
                client.BaseAddress = new Uri("Rebrickable:https://rebrickable.com/api/v3/");
                var apiKey = builder.Configuration["Rebrickable:ApiKey"];
                client.DefaultRequestHeaders.Add("Authorization", $"key {apiKey}");
            });

            
            // Rebrickable Settings
            builder.Services.AddSingleton<RebrickableSettings>(provider =>
            {
                string apiKey = builder.Configuration.GetValue<string>("Rebrickable:ApiKey");
                var rebrickableSettings = new RebrickableSettings
                {
                    ApiKey = apiKey
                };
                return rebrickableSettings;
            });

            


            builder.Services.AddRazorPages();


            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<LegoDbContext>();
            //    DbInitializer.Initialize(dbContext);
            //}

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
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            

            //delete all records from the database
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LegoDbContext>();
                dbContext.LegoSets.RemoveRange(dbContext.LegoSets);
                dbContext.SaveChanges();
            }

            app.Run();

        }
    }
}