using BuildingExample.DTOs;

namespace BuildingExample.Services
{
    public interface IApartmentService
    {
        Task<ApartmentDetailsDTO> GetOne(int id);
        Task<List<ApartmentViewDTO>> GetAll();
        Task<List<ApartmentCustomDTO>> GetAllCustom();
        Task<ApartmentDetailsDTO> Add(ApartmentCreateDTO dto);
        Task Delete(int id);
        Task<ApartmentDetailsDTO> Update(int id, ApartmentUpdateDTO dto);
        Task<List<ApartmentViewDTO>> SearchByArea(double from, double to);
        Task<List<ApartmentViewDTO>> SearchByFloorAndBuilding(double floorFrom, double floorTo, int buildingId);
    }
}
