using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var connString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var dbName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var collName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
            var client = new MongoClient(connString);
            var database = client.GetDatabase(dbName);
            Products = database.GetCollection<Product>(collName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
