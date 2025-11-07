using BuildingExample.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingExample.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly AppDbContext _dbContext;
        public BuildingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Building?> GetOne(int id)
        {
            return await _dbContext.Buildings.FindAsync(id);
        }

        public async Task<List<Building>> GetAll()
        {
            return await _dbContext.Buildings.ToListAsync();
        }

        public async Task Add(Building entity)
        {
            _dbContext.Buildings.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Building entity)
        {
            _dbContext.Buildings.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Building entity)
        {
            _dbContext.Buildings.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
