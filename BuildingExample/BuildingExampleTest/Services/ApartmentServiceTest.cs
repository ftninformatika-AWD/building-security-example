using AutoMapper;
using BuildingExample.DTOs;
using BuildingExample.Exceptions;
using BuildingExample.Models;
using BuildingExample.Repositories;
using BuildingExample.Services;
using BuildingExample.Settings;
using NSubstitute;

namespace BuildingExampleTest.Services
{
    public class ApartmentServiceTest
    {
        [Fact]
        public async void GetOne_ValidId_ReturnsObject()
        {
            // Arrange
            Building building = new Building()
            {
                Id = 1,
                Address = "Baker St 22",
                Floors = 2,
                HasElevator = true,
                YearOfConstruction = 2023
            };

            Apartment apartment = new Apartment()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = building.Id,
                Building = building,
            };

            ApartmentDetailsDTO apartmentResult = new ApartmentDetailsDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = building.Id,
                BuildingAddress = building.Address,
            };

            var mockApartmentRepository = Substitute.For<IApartmentRepository>();
            mockApartmentRepository.GetOne(12).Returns(apartment);

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var service = new ApartmentService(mockApartmentRepository, mapper);

            // Act
            var result = await service.GetOne(apartment.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(apartmentResult, result); // Requires Equals in ApartmentDetailsDTO class
        }

        [Fact]
        public async void GetOne_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);

            var mockApartmentRepository = Substitute.For<IApartmentRepository>();
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var service = new ApartmentService(mockApartmentRepository, mapper);

            // Act
            var act = () => service.GetOne(12);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void Update_ValidApartment_ReturnsObject()
        {
            // Arrange
            ApartmentUpdateDTO updateDto = new ApartmentUpdateDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = 11,
            };

            ApartmentDetailsDTO apartmentResult = new ApartmentDetailsDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = 11,
                BuildingAddress = "Baker St 21",
            };

            var mockApartmentRepository = Substitute.For<IApartmentRepository>();
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var service = new ApartmentService(mockApartmentRepository, mapper);

            // Act
            var result = await service.Update(updateDto.Id, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(apartmentResult, result); // Requires Equals in ApartmentDetailsDTO class
        }

        [Fact]
        public async void Update_InvalidData_ThrowsBadRequestException()
        {
            // Arrange
            ApartmentUpdateDTO updateDto = new ApartmentUpdateDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = 11,
            };
            BadRequestException mockException = new BadRequestException("Identifier value is invalid.");

            var mockApartmentRepository = Substitute.For<IApartmentRepository>();
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var service = new ApartmentService(mockApartmentRepository, mapper);

            // Act
            var act = () => service.Update(23, updateDto);

            // Assert
            BadRequestException exception = await Assert.ThrowsAsync<BadRequestException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void Delete_ValidId_Finishes()
        {
            // Arrange
            Building building = new Building()
            {
                Id = 1,
                Address = "Baker St 22",
                Floors = 2,
                HasElevator = true,
                YearOfConstruction = 2023
            };

            Apartment apartment = new Apartment()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = building.Id,
                Building = building,
            };

            var mockApartmentRepository = Substitute.For<IApartmentRepository>();
            mockApartmentRepository.GetOne(12).Returns(apartment);

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var service = new ApartmentService(mockApartmentRepository, mapper);

            // Act
            await service.Delete(apartment.Id);

            // Assert
            await mockApartmentRepository.Received().Delete(apartment); // check if function was called
        }

        [Fact]
        public async void Delete_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);

            var mockApartmentRepository = Substitute.For<IApartmentRepository>();
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var service = new ApartmentService(mockApartmentRepository, mapper);

            // Act
            var act = () => service.Delete(12);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }
    }
}
