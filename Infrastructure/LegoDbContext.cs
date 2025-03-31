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

        public DbSet<DbLegoSet> DbLegoSets { get; set; }
        public DbSet<DbLegoPart> DbLegoParts { get; set; }
        public DbSet<Imported_sets> ImportedSets { get; set; }
        public DbSet<Imported_parts> ImportedParts { get; set; }
        public DbSet<Imported_inventory_sets> ImportedInventorySets { get; set; }
        public DbSet<Imported_inventory_parts> ImportedInventoryParts { get; set; }
        public DbSet<Imported_Inventories> ImportedInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbLegoSet>().HasKey(s => s.Id);
            modelBuilder.Entity<DbLegoSet>().Property(s => s.SetNum).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Imported_sets>().HasKey(s => s.set_num);
            modelBuilder.Entity<Imported_parts>().HasKey(p => p.part_num);
            modelBuilder.Entity<Imported_inventory_sets>().HasKey(i => new { i.inventory_id, i.set_num });
            modelBuilder.Entity<Imported_inventory_parts>().HasKey(i => new { i.inventory_id, i.part_num });
            modelBuilder.Entity<Imported_Inventories>().HasKey(i => i.Id);

            modelBuilder.Entity<Imported_Inventories>()
        .HasOne<Imported_sets>()
        .WithMany()
        .HasForeignKey(i => i.set_num)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Imported_inventory_parts>()
                .HasOne<Imported_parts>()
                .WithMany()
                .HasForeignKey(i => i.part_num)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Imported_inventory_parts>()
                .HasOne<Imported_Inventories>()
                .WithMany()
                .HasForeignKey(i => i.inventory_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Imported_inventory_sets>()
                .HasOne<Imported_Inventories>()
                .WithMany()
                .HasForeignKey(i => i.inventory_id)
                .OnDelete(DeleteBehavior.Cascade);
           
            modelBuilder.Entity<Imported_inventory_sets>()
                .HasOne<Imported_sets>()
                .WithMany()
                .HasForeignKey(i => i.set_num)
                .OnDelete(DeleteBehavior.Cascade);
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
