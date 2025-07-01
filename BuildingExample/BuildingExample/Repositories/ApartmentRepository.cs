using BuildingExample.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingExample.Repositories
{
    public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public new async Task<List<Apartment>> GetAll()
        {
            return await _dbContext.Apartments
                .Include(a => a.Building)
                .OrderBy(a => a.Area)
                .ToListAsync();
        }

        public new async Task<Apartment?> GetOne(int id)
        {
            return await _dbContext.Apartments
                .Include(a => a.Building)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public new async Task Add(Apartment apartment)
        {
            _dbContext.Apartments.Add(apartment);
            await _dbContext.Entry(apartment).Reference(a => a.Building).LoadAsync();
            await _dbContext.SaveChangesAsync();
        }

        public new async Task Update(Apartment apartment)
        {
            _dbContext.Apartments.Update(apartment);
            await _dbContext.Entry(apartment).Reference(a => a.Building).LoadAsync();
            await _dbContext.SaveChangesAsync();
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
    }
}
