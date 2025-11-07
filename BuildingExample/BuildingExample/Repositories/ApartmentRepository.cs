using BuildingExample.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingExample.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _dbContext;
        public ApartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Apartment>> SearchByArea(double from, double to)
        {
            return await _dbContext.Apartments
                .Include(a => a.Building)
                .Where(a => a.Area >= from && a.Area <= to)
                .OrderBy(a => a.Area)
                .ToListAsync();
        }

        public async Task<List<Apartment>> SearchByFloorAndBuilding(double floorFrom, double floorTo, int buildingId)
        {
            return await _dbContext.Apartments
                .Include(a => a.Building)
                .Where(a => a.Floor >= floorFrom && a.Floor <= floorTo && a.BuildingId == buildingId)
                .OrderByDescending(a => a.BuildingId)
                .ToListAsync();
        }

        public async Task<List<Apartment>> GetAll()
        {
            return await _dbContext.Apartments
                .Include(a => a.Building)
                .OrderBy(a => a.Area)
                .ToListAsync();
        }

        public async Task<Apartment?> GetOne(int id)
        {
            return await _dbContext.Apartments
                .Include(a => a.Building)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Apartment apartment)
        {
            _dbContext.Apartments.Add(apartment);
            await _dbContext.Entry(apartment).Reference(a => a.Building).LoadAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Apartment apartment)
        {
            _dbContext.Apartments.Update(apartment);
            await _dbContext.Entry(apartment).Reference(a => a.Building).LoadAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Apartment entity)
        {
            _dbContext.Set<Apartment>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
