using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBrickVault.Core.Models;

namespace TheBrickVault.Infrastructure.Data
{
    public class LegoDbContext : DbContext
    {
        public LegoDbContext(DbContextOptions<LegoDbContext> options) : base(options) 
        {
            LegoSets = Set<LegoSet>();
        }

        public DbSet<LegoSet> LegoSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegoSet>().HasKey(s => s.Id);
            modelBuilder.Entity<LegoSet>().Property(s => s.SetNum).IsRequired().HasMaxLength(20); 


            //Seed data for testing
            //modelBuilder.Entity<LegoSet>().HasData(
            //    new LegoSet { Id = 6, SetNum = "123", Name = "Test Set" },
            //    new LegoSet { Id = 7, SetNum = "5678", Name = "Another Test Set" },
            //    new LegoSet { Id = 8, SetNum = "91011593598", Name = "Yet Another Test Set" },
            //    new LegoSet { Id = 9, SetNum = "1", Name = "" }
        //);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}

