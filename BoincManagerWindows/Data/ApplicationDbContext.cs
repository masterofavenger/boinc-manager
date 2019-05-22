﻿using Microsoft.EntityFrameworkCore;

namespace BoincManagerWindows.Models
{
    // From: https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BoincManager.Models.Host> Host { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(BoincManager.Constants.SqlightConnectionString);
        }
    }
}