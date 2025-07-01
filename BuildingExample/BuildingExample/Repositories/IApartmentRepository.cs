using BuildingExample.Models;

namespace BuildingExample.Repositories
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        Task<List<Apartment>> SearchByArea(double from, double to);
        Task<List<Apartment>> SearchByFloorAndBuilding(double floorFrom, double floorTo, int buildingId);
    }
}
