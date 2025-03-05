using Microsoft.EntityFrameworkCore;
using TheBrickVault.Infrastructure.Data;
using TheBrickVault.Core.Models;
using System;


class ConsoleApp
{
    static void Main(string[] args)
    {
        //just to see if the console will respond.
        Console.WriteLine("....");

        var optionsBuilder = new DbContextOptionsBuilder<LegoDbContext>();
        optionsBuilder.UseSqlite("Data Source=./TheBrickVault.db");

        using var context = new LegoDbContext(optionsBuilder.Options);

        var newSet = new LegoSet
        {
            Id = 1,
            SetNum = "1234",
            Name = "Test Set",

        };

        context.LegoSets.Add(newSet);
        //Debugging line
        Console.WriteLine($"Database path: {context.Database.GetDbConnection().ConnectionString}");
        using (var connection = context.Database.GetDbConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='LegoSets'";
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Existing tables in the database:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                    }
                    
                }
            }
            Console.WriteLine($"Database connection: {connection.ConnectionString}");
        }
        context.SaveChanges();

        Console.WriteLine("Set added to the database.");

        var sets = context.LegoSets.ToList();
        foreach (var set in sets)
        {
            Console.WriteLine($"Set Number: {set.SetNum}, Name: {set.Name}");
        }

        


    }
}