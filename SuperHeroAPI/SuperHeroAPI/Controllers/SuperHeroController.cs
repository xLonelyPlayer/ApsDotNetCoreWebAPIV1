using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperHeroAPI.Data;
using SuperHeroAPI.Repositories;
using SuperHeroAPI.Models;
using SuperHeroAPI.Repositories.Interfaces;
using System.Text.Json;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRespository repo;

        public SuperHeroController(ISuperHeroRespository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return await repo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(string id)
        {
            var hero = await repo.GetAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero([FromBody] SuperHero newHero)
        {
            var addedHero = await repo.AddAsync(newHero);
            return Ok(addedHero);
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero([FromBody] SuperHero updateHero)
        {
            var updatedHero = await repo.UpdateAsync(updateHero);
            return Ok(updatedHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> DeleteHero(string id)
        {
            try
            {
                if (!await repo.DeleteAsync(id))
                    return NotFound();
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
