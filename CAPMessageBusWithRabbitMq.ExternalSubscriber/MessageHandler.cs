using System;
using CAPMessageBusWithRabbitMq.Core;
using DotNetCore.CAP;

namespace CAPMessageBusWithRabbitMq.ExternalSubscriber
{
    public class MessageHandler : ICapSubscribe
    {
        private readonly GeoDataSerialisationService _serialisationService;

        public MessageHandler(GeoDataSerialisationService serialisationService)
        {
            _serialisationService = serialisationService;
        }

        [CapSubscribe(nameof(TripStatusMessage))]
        public void HandleTripStatusMessage(string payload)
        {
            if (payload == null || string.IsNullOrWhiteSpace(payload)) throw new ArgumentNullException(nameof(payload));

            var message =
                _serialisationService.DeSerialise<TripStatusMessage>(payload);
            Console.WriteLine($"TripId: {message.TripId} TripStatus: {message.TripStatus} Time:{message.Time}");
        }
    }
}