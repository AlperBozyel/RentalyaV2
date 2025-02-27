using MongoDB.Bson.Serialization.Attributes;
using RentalyaAPI.Models.Base;
using RentalyaAPI.Attributes;

namespace RentalyaAPI.Models
{
    [BsonCollection("users")]
    public class ApplicationUser : BaseEntity
    {
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [BsonElement("lastName")]
        public string LastName { get; set; } = string.Empty;

        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; }

        [BsonElement("roles")]
        public List<string> Roles { get; set; } = new();
    }
}