using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SuperHeroAPI.Data;
using SuperHeroAPI.Repositories.Interfaces;

namespace SuperHeroAPI.Repositories
{
    public class SuperHeroRepositorieMongoDB : ISuperHeroRespositorie
    {
        private readonly IMongoCollection<SuperHero> _heroes;

        public SuperHeroRepositorieMongoDB(IOptions<SuperHeroDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _heroes = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<SuperHero>(options.Value.SuperHeroCollection);
        }

        public async Task<SuperHero?> GetAsync(string id)
        {
            var hero = await _heroes.Find(m => m.Id == id).FirstOrDefaultAsync();
            return hero;
        }

        public async Task<List<SuperHero>> GetAll()
        {
            return await _heroes.Find(_ => true).ToListAsync();
        }

        public async Task<SuperHero> AddAsync(SuperHero hero)
        {
            hero.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            hero.created_at = DateTime.Now;
            await _heroes.InsertOneAsync(hero);
            return hero;
        }

        public async Task<SuperHero> UpdateAsync(SuperHero updateHero)
        {
            updateHero.updated_at = DateTime.Now;
            await _heroes.ReplaceOneAsync(m => m.Id == updateHero.Id, updateHero);
            return updateHero;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deletedHero = await _heroes.DeleteOneAsync(m => m.Id == id);
            return deletedHero.DeletedCount > 0;
        }
    }
}
