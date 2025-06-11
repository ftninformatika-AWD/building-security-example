using BuildingExample.Models;

namespace BuildingExample.Repositories
{
    public class BuildingRepository : GenericRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
