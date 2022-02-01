using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SuperHeroAPI.Data;
using System.Text.Json;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IMongoCollection<SuperHero> _heroes;

        public SuperHeroController(IOptions<SuperHeroDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _heroes = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<SuperHero>(options.Value.SuperHeroCollection);
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return await _heroes.Find(_ => true).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(string id)
        {
            var hero = _heroes.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody] SuperHero newHero)
        {
            newHero.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            await _heroes.InsertOneAsync(newHero);
            return Ok(JsonSerializer.Serialize(newHero));
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updateHero)
        {
            await _heroes.ReplaceOneAsync(m => m.Id == updateHero.Id, updateHero);
            return Ok(JsonSerializer.Serialize(updateHero));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(string id)
        {
            var hero = _heroes.DeleteOneAsync(m => m.Id == id);
            return Ok(_heroes);
        }
    }
}
