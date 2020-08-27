using DeliveryApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryApi.Services
{
    public class EmpleadoService
    {
        private readonly IMongoCollection<Empleado> _empleados;

        public EmpleadoService(IDeliveryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _empleados = database.GetCollection<Empleado>(settings.EmpleadosCollectionName);
        }

        public List<Empleado> Get() =>
            _empleados.Find(empleado => true).ToList();

        public Empleado Get(string id) =>
            _empleados.Find<Empleado>(empleado => empleado.Id == id).FirstOrDefault();

        public Empleado Create(Empleado empleado)
        {
            _empleados.InsertOne(empleado);
            return empleado;
        }

        public void Update(string id, Empleado empleadoIn) =>
            _empleados.ReplaceOne(empleado => empleado.Id == id, empleadoIn);

        public void Remove(Empleado empleadoIn) =>
            _empleados.DeleteOne(empleado => empleado.Id == empleadoIn.Id);

        public void Remove(string id) => 
            _empleados.DeleteOne(empleado => empleado.Id == id);
    }
}