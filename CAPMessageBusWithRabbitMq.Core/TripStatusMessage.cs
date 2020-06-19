using System;
using NetTopologySuite.Geometries;

namespace CAPMessageBusWithRabbitMq.Core
{
    public class TripStatusMessage
    {
        public long TripId { get; set; }
        public Point CurrentLocation { get; set; }
        public TripStatus TripStatus { get; set; }
        public DateTime Time { get; set; }
    }
}