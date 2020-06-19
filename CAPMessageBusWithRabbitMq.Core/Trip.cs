using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace CAPMessageBusWithRabbitMq.Core
{
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
}