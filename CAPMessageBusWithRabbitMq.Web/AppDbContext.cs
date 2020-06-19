using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

namespace CAPMessageBusWithRabbitMq.Web
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.Entity<Trip>().Property(x => x.Status).HasConversion(new EnumToStringConverter<TripStatus>());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Trip
    {
        public long Id { get; set; }
        [StringLength(30)] public string DriverId { get; set; }

        [Column(TypeName = "geometry (point)")]
        public Point StartingPoint { get; set; }

        [Column(TypeName = "geometry (point)")]
        public Point Destination { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [StringLength(10)] public TripStatus Status { get; set; }
    }

    public enum TripStatus
    {
        Enroute = 1,
        Completed = 2,
        Cancelled = 3,
        Requested = 4
    }

    public class TripStatusMessage
    {
        public long TripId { get; set; }
        public Point CurrentLocation { get; set; }
        public TripStatus TripStatus { get; set; }
        public DateTime Time { get; set; }
    }
}