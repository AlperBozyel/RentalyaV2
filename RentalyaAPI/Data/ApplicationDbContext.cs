using MongoDB.Driver;
using RentalyaAPI.Models;
using Microsoft.Extensions.Options;
using RentalyaAPI.Configurations;

namespace RentalyaAPI.Data
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<ApplicationUser> Users => 
            _database.GetCollection<ApplicationUser>("Users");

        // Diğer koleksiyonlar buraya eklenecek
        public IMongoCollection<Property> Properties =>
            _database.GetCollection<Property>("Properties");
            
        public IMongoCollection<Category> Categories =>
            _database.GetCollection<Category>("Categories");
    }
}