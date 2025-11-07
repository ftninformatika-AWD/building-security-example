namespace BuildingExample.Domain
{
    public interface IApartmentRepository
    {
        Task<List<Apartment>> SearchByArea(double from, double to);
        Task<List<Apartment>> SearchByFloorAndBuilding(double floorFrom, double floorTo, int buildingId);
        Task<Apartment?> GetOne(int id);
        Task<List<Apartment>> GetAll();
        Task Add(Apartment entity);
        Task Delete(Apartment entity);
        Task Update(Apartment entity);
    }
}
