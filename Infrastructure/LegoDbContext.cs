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

        public DbSet<LegoSet> LegoSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegoSet>().HasKey(s => s.Id);
        }
    }
}

