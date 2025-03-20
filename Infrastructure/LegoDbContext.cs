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
        public LegoDbContext(DbContextOptions<LegoDbContext> options) : base(options) { }
        //{ 
        //    DbLegoSets = Set<DbLegoSet>();
        //    DbLegoParts = Set<DbLegoPart>();
        //}

        public DbSet<DbLegoSet> DbLegoSets { get; set; }
        public DbSet<DbLegoPart> DbLegoParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbLegoSet>().HasKey(s => s.Id);
            modelBuilder.Entity<DbLegoSet>().Property(s => s.SetNum).IsRequired().HasMaxLength(20);
           
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

