using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CAPMessageBusWithRabbitMq.Web.Services;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace CAPMessageBusWithRabbitMq.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        int count = 0;
        private readonly ICapPublisher _publisher;
        private readonly AppDbContext _context;
        private readonly GeoDataSerialisationService _serialisationService;

        public WeatherForecastController(ICapPublisher publisher,AppDbContext context,GeoDataSerialisationService serialisationService)
        {
            _publisher = publisher;
            _context = context;
            _serialisationService = serialisationService;
        }

       [HttpGet("RequestTrip")] public async Task<IActionResult> RequestTrip()
       {
           count++;
           var tripStatusFromCount =count % 3;
            try
            {
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                await using var transaction = _context.Database.BeginTransaction(_publisher);
                //add a new trip
                var trip = new Trip
                {
                    StartingPoint = geometryFactory.CreatePoint(new Coordinate(5.6353201, -0.0653353)),
                    Destination = geometryFactory.CreatePoint(new Coordinate(5.6424616, -0.0590958)),
                    StartTime = DateTime.UtcNow,
                    Status = TripStatus.Requested
                };
                //save the trip
              var entityEntry=  await _context.Trips.AddAsync(trip);
              trip = entityEntry.Entity;

                await _context.SaveChangesAsync();

                //publish trip request message for anyone interested
                var tripStatusMessage = new TripStatusMessage
                {
                    CurrentLocation = geometryFactory.CreatePoint(new Coordinate(5.6353201, -0.0653353)),
                    Time = DateTime.UtcNow,
                    TripId = trip.Id,
                    TripStatus =(TripStatus)tripStatusFromCount
                };
                var stringMessage = _serialisationService.Serialise(geometryFactory, tripStatusMessage);
                await _publisher.PublishAsync(nameof(TripStatusMessage),tripStatusMessage);
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

       
    }
}
