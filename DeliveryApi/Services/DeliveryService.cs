using DeliveryApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryApi.Services
{
    public class DeliveryService
    {
        private readonly IMongoCollection<Delivery> _deliverys;

        public DeliveryService(IDeliveryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _deliverys = database.GetCollection<Delivery>(settings.DeliverysCollectionName);
        }

        public List<Delivery> Get() =>
            _deliverys.Find(delivery => true).ToList();

        public Delivery Get(string id) =>
            _deliverys.Find<Delivery>(delivery => delivery.Id == id).FirstOrDefault();

        public Delivery Create(Delivery delivery)
        {
            _deliverys.InsertOne(delivery);
            return delivery;
        }

        public void Update(string id, Delivery deliveryIn) =>
            _deliverys.ReplaceOne(delivery => delivery.Id == id, deliveryIn);

        public void Remove(Delivery deliveryIn) =>
            _deliverys.DeleteOne(delivery => delivery.Id == deliveryIn.Id);

        public void Remove(string id) => 
            _deliverys.DeleteOne(delivery => delivery.Id == id);
    }
}