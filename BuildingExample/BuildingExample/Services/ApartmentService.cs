using AutoMapper;
using BuildingExample.DTOs;
using BuildingExample.Exceptions;
using BuildingExample.Models;
using BuildingExample.Repositories;

namespace BuildingExample.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;

        public ApartmentService(IApartmentRepository apartmentRepository, IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
        }

        public async Task<ApartmentDetailsDTO> Add(ApartmentCreateDTO dto)
        {
            var apartment = _mapper.Map<Apartment>(dto);
            await _apartmentRepository.Add(apartment);
            return _mapper.Map<ApartmentDetailsDTO>(apartment);
        }

        public async Task Delete(int id)
        {
            var apartment = await _apartmentRepository.GetOne(id);
            if (apartment == null)
            {
                throw new NotFoundException(id);
            }
            await _apartmentRepository.Delete(apartment);
        }

        public async Task<List<ApartmentViewDTO>> GetAll()
        {
            var apartments = await _apartmentRepository.GetAll();
            return apartments
                .Select(_mapper.Map<ApartmentViewDTO>)
                .ToList();
        }

        public async Task<List<ApartmentCustomDTO>> GetAllCustom()
        {
            var apartments = await _apartmentRepository.GetAll();
            return apartments
                .Select(_mapper.Map<ApartmentCustomDTO>)
                .ToList();
        }

        public async Task<ApartmentDetailsDTO> GetOne(int id)
        {
            var apartment = await _apartmentRepository.GetOne(id);
            if (apartment == null)
            {
                throw new NotFoundException(id);
            }
            return _mapper.Map<ApartmentDetailsDTO>(apartment);
        }

        public async Task<ApartmentDetailsDTO> Update(int id, ApartmentUpdateDTO dto)
        {
            if (id != dto.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            Apartment apartment = _mapper.Map<Apartment>(dto);
               
            await _apartmentRepository.Update(apartment);
            return _mapper.Map<ApartmentDetailsDTO>(apartment);
        }

        public async Task<List<ApartmentViewDTO>> SearchByArea(double from, double to)
        {
            var apartments = await _apartmentRepository.SearchByArea(from, to);
            return apartments
                .Select(_mapper.Map<ApartmentViewDTO>)
                .ToList();
        }

        public async Task<List<ApartmentViewDTO>> SearchByFloorAndBuilding(double floorFrom, double floorTo, int buildingId)
        {
            var apartments = await _apartmentRepository.SearchByFloorAndBuilding(floorFrom, floorTo, buildingId);
            return apartments
                .Select(_mapper.Map<ApartmentViewDTO>)
                .ToList();
        }
    }
}
