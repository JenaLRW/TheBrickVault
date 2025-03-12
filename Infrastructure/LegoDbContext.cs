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
        public DbSet<LegoPart> LegoParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegoSet>().HasKey(s => s.Id);
            modelBuilder.Entity<LegoSet>().Property(s => s.SetNum).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<LegoPart>().HasOne(LegoPart => LegoPart.LegoSet)
                .WithMany(LegoSet => LegoSet.Parts)
                .HasForeignKey(LegoPart => LegoPart.SetNum)
                .HasPrincipalKey(LegoSet => LegoSet.SetNum);
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

