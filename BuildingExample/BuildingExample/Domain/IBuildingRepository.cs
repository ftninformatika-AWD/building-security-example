namespace BuildingExample.Domain
{
    public interface IBuildingRepository
    {
        Task<Building?> GetOne(int id);
        Task<List<Building>> GetAll();
        Task Add(Building entity);
        Task Delete(Building entity);
        Task Update(Building entity);
    }
}
