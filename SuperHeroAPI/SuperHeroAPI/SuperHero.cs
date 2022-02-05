using MongoDB.Bson.Serialization.Attributes;

namespace SuperHeroAPI
{
    public class SuperHero
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string? Multiverse { get; set; } = string.Empty;
        public DateTime? created_at { get; set; } = DateTime.MinValue;
        public DateTime? updated_at { get; set; } = DateTime.MinValue;
    }
}
