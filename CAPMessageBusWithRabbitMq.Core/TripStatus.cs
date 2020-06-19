namespace CAPMessageBusWithRabbitMq.Core
{
    public enum TripStatus
    {
        Enroute = 1,
        Completed = 2,
        Cancelled = 3,
        Requested = 4
    }
}