using System;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace CAPMessageBusWithRabbitMq.Web.Services
{
    public class MessageHandlers : ICapSubscribe
    {
        private readonly GeoDataSerialisationService _serialisationService;
        private readonly AppDbContext _context;

        public MessageHandlers(GeoDataSerialisationService serialisationService, AppDbContext context)
        {
            _serialisationService = serialisationService;
            _context = context;
        }

        [CapSubscribe(nameof(TripStatusMessage))]
        public async Task HandleTripStatusMessage(string payload)
        {
            if (payload == null||string.IsNullOrWhiteSpace(payload)) throw new ArgumentNullException(nameof(payload));

            var message =
                _serialisationService.DeSerialise<TripStatusMessage>(payload);
            await using (_context)
            {
                var trip = await _context.Trips.FindAsync(message.TripId);
                if (trip != null && trip.Status != message.TripStatus)
                {
                    trip.Status = message.TripStatus;
                    _context.Trips.Update(trip);
                   await _context.SaveChangesAsync();
                }
            }
        }
    }
}