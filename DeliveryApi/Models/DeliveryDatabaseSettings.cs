namespace DeliveryApi.Models
{
    public class DeliveryDatabaseSettings : IDeliveryDatabaseSettings
    {
        public string ClientesCollectionName { get; set; }
        public string EmpleadosCollectionName { get; set; }
        public string DeliverysCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDeliveryDatabaseSettings
    {
        string ClientesCollectionName { get; set; }
        string EmpleadosCollectionName { get; set; }
        string DeliverysCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}