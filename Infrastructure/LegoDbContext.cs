﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("Data Source");
                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}

