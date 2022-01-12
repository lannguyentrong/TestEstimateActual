using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEstimateActual.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Actual> Actuals { get; set; }
        public DbSet<Estimate> Estimates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("TestEstimateActualConnectionString");
                //optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actual>()
                .Property(item => item.State)
                .ValueGeneratedNever();

            // Set PK For "Estimate"
            modelBuilder.Entity<Estimate>().HasKey(item => new { item.State, item.District }).HasName("estimate_pk");
        }
    }
}
