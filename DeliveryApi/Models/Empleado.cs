using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DeliveryApi.Models
{
    public class Empleado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nombre")]
        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [BsonElement("Apellido")]
        [JsonProperty("Apellido")]
        public string Apellido { get; set; }

        [BsonElement("Ciudad")]
        [JsonProperty("Ciudad")]
        public string Ciudad { get; set; }

        [BsonElement("Direccion")]
        [JsonProperty("Direccion")]
        public string Direccion { get; set; }

        [BsonElement("Cedula")]
        [JsonProperty("Cedula")]
        public string Cedula { get; set; }

        [BsonElement("Telefono")]
        [JsonProperty("Telefono")]
        public string Telefono { get; set; }
        
        [BsonElement("estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}