using AutoMapper;
using BuildingExample.DTOs;
using BuildingExample.Models;

namespace BuildingExample.Settings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Apartment, ApartmentViewDTO>();
            CreateMap<Apartment, ApartmentDetailsDTO>();
            CreateMap<Apartment, ApartmentCustomDTO>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => $"{src.Building!.Address} ({src.Building!.YearOfConstruction})"))
                .ForMember(dest => dest.Building, opt => opt.MapFrom(src => $"Floors: {src.Building!.Floors}; Elevator: {(src.Building!.HasElevator ? "Yes" : "No") }"));

            CreateMap<ApartmentCreateDTO, Apartment>();
            CreateMap<ApartmentUpdateDTO, Apartment>();
        }
    }
}
