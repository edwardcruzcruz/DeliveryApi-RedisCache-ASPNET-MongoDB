using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DeliveryApi.Models
{
    public class Delivery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Titulo")]
        [JsonProperty("Titulo")]
        public string Titulo { get; set; }

        [BsonElement("Descripcion")]
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("Direccion")]
        [JsonProperty("Direccion")]
        public string Direccion { get; set; }

        [BsonElement("FechaPedido")]
        [JsonProperty("FechaPedido")]
        public DateTime FechaPedido { get; set; }

        [BsonElement("FechaEntrega")]
        [JsonProperty("FechaEntrega")]
        public DateTime FechaEntrega { get; set; }

        [BsonElement("Valor")]
        [JsonProperty("Valor")]
        public float Valor { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Empleado_id")]
        [JsonProperty("Empleado_id")]
        public string Empleado_id{get; set;}

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Cliente_id")]
        [JsonProperty("Cliente_id")]
        public string Cliente_id{get; set;}

        [BsonElement("estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }
    }
}