using BuildingExample.Models;

namespace BuildingExample.Services
{
    public interface IBuildingService
    {
        Task<Building> GetOne(int id);
        Task<List<Building>> GetAll();
        Task<Building> Add(Building building);
        Task Delete(int id);
        Task<Building> Update(int id, Building building);
    }
}
