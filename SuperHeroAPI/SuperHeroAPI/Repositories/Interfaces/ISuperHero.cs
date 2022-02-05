using SuperHeroAPI.Models;

namespace SuperHeroAPI.Repositories.Interfaces
{
    public interface ISuperHeroRespository
    {
        Task<List<SuperHero>> GetAll();
        Task<SuperHero?> GetAsync(string id);
        Task<SuperHero> AddAsync(SuperHero hero);
        Task<SuperHero> UpdateAsync(SuperHero hero);
        Task<bool> DeleteAsync(string id);
    }
}
