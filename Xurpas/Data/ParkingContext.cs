using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xurpas.Models;

namespace Xurpas.Data
{
    public class ParkingContext : DbContext
    {
        //public ParkingContext()
        //{
        //}

        public ParkingContext(DbContextOptions<ParkingContext> options) : base(options)
        {
        }

        public DbSet<Parking> Parking { get; set; }
        public DbSet<ParkingType> ParkingType { get; set; }
        public DbSet<EntryPoint> EntryPoint { get; set; }
        public DbSet<ParkingSpace> ParkingSpace { get; set; }
        public DbSet<ParkingSpacePerEntryPoint> ParkingSpacePerEntryPoint { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>().ToTable("Parking");
            modelBuilder.Entity<EntryPoint>().ToTable("EntryPoint");
            modelBuilder.Entity<ParkingSpace>().ToTable("ParkingSpace");
            modelBuilder.Entity<ParkingType>().ToTable("ParkingType");
            modelBuilder.Entity<ParkingSpacePerEntryPoint>().ToTable("ParkingSpacePerEntryPoint");
        }

      
    }
}
