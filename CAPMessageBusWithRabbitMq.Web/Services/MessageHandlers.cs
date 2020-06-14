namespace CAPMessageBusWithRabbitMq.Web.Services
{
    public class MessageHandlers
    {
        private readonly GeoDataSerialisationService _serialisationService;

        public MessageHandlers(GeoDataSerialisationService serialisationService)
        {
            _serialisationService = serialisationService;
        }

        public void HandleTripStatusMessage()
        {

        }
    }
}