namespace SuperHeroAPI.Repositories.Interfaces
{
    public interface ISuperHeroRespositorie
    {
        Task<List<SuperHero>> GetAll();
        Task<SuperHero?> GetAsync(string id);
        Task<SuperHero> AddAsync(SuperHero hero);
        Task<SuperHero> UpdateAsync(SuperHero hero);
        Task<bool> DeleteAsync(string id);
    }
}
