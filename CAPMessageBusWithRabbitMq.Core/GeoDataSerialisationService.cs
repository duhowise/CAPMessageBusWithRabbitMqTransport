using System.IO;
using System.Text;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace CAPMessageBusWithRabbitMq.Core
{
    public class GeoDataSerialisationService
    {
        public string Serialise<T>(GeometryFactory geometryFactory, T tripStatusMessage)
        {
            var stringBuilder = new StringBuilder();
            var serializer = GeoJsonSerializer.Create(geometryFactory);
            serializer.Serialize(new JsonTextWriter(new StringWriter(stringBuilder)), tripStatusMessage,
                typeof(T));
            return stringBuilder.ToString();
        }
        
        public T DeSerialise<T>(string payload)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var serializer = GeoJsonSerializer.Create(geometryFactory);
         var data=   serializer.Deserialize<T>(new JsonTextReader(new StringReader(payload)));
            return data;
        }
    }
}