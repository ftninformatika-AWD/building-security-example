using BuildingExample.Controllers;
using BuildingExample.DTOs;
using BuildingExample.Exceptions;
using BuildingExample.Models;
using BuildingExample.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BuildingExampleTest.Controllers
{
    public class ApartmentsControllerTest
    {
        [Fact]
        public async void GetApartments_Success_ReturnsObjects()
        {
            // Arrange
            ApartmentViewDTO apartment1 = new ApartmentViewDTO()
            {
                Id = 12,
                Area = 21,
                BuildingAddress = "Baker St. 23",
                NumberOfRooms = 1
            };

            ApartmentViewDTO apartment2 = new ApartmentViewDTO()
            {
                Id = 25,
                Area = 44,
                BuildingAddress = "Marshall St. 11",
                NumberOfRooms = 2
            };

            var apartments = new List<ApartmentViewDTO>() { apartment1, apartment2 };

            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.GetAll().Returns(apartments);

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var actionResult = await controller.GetApartments() as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var dtoResult = (List<ApartmentViewDTO>)actionResult.Value;

            for (int i = 0; i < dtoResult.Count; i++)
            {
                Assert.Equal(apartments[i], dtoResult[i]);
            }
        }

        [Fact]
        public async void GetApartment_ValidId_ReturnsObject()
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

            ApartmentDetailsDTO apartment = new ApartmentDetailsDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = building.Id,
                BuildingAddress = building.Address,
            };

            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.GetOne(12).Returns(apartment);

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var actionResult = await controller.GetApartment(12) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var dtoResult = (ApartmentDetailsDTO)actionResult.Value;
            Assert.Equal(apartment, dtoResult);
        }

        [Fact]
        public async void GetApartment_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);
            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.When(x => x.GetOne(12)).Do(x => { throw mockException; });

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var act = () => controller.GetApartment(12);

            // Assert
            NotFoundException exception = await  Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void PostApartment_ValidApartment_SetsLocationHeader()
        {
            // Arrange
            ApartmentCreateDTO createDto = new ApartmentCreateDTO()
            {
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = 11,
            };

            ApartmentDetailsDTO resultDto = new ApartmentDetailsDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = 11,
                BuildingAddress = "Baker St 21",
            };

            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.Add(createDto).Returns(resultDto);

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var actionResult = await controller.PostApartment(createDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal("GetApartment", actionResult.ActionName);
            Assert.Equal(resultDto.Id, actionResult.RouteValues["id"]);
            Assert.Equal(resultDto, actionResult.Value);
        }

        [Fact]
        public async void PutApartment_ValidApartment_ReturnsObject()
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

            ApartmentDetailsDTO resultDto = new ApartmentDetailsDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = 11,
                BuildingAddress = "Baker St 21",
            };

            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.Update(updateDto.Id, updateDto).Returns(resultDto);

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var actionResult = await controller.PutApartment(updateDto.Id, updateDto) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var dtoResult = (ApartmentDetailsDTO)actionResult.Value;
            Assert.Equal(resultDto, dtoResult);
        }

        [Fact]
        public async void PutApartment_InvalidData_ThrowsBadRequestException()
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

            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.When(x => x.Update(23, updateDto)).Do(x => { throw mockException; });

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var act = () => controller.PutApartment(23, updateDto);

            // Assert
            BadRequestException exception = await Assert.ThrowsAsync<BadRequestException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void DeleteApartment_ValidId_ReturnsNoContent()
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

            ApartmentDetailsDTO apartment = new ApartmentDetailsDTO()
            {
                Id = 12,
                Area = 21,
                NumberOfRooms = 1,
                ApartmentNumber = "23",
                Floor = 2,
                BuildingId = building.Id,
                BuildingAddress = building.Address,
            };

            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.GetOne(12).Returns(apartment);

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var actionResult = await controller.DeleteApartment(12) as NoContentResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public async void DeleteApartment_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);
            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.When(x => x.Delete(12)).Do(x => { throw mockException; });

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var act = () => controller.DeleteApartment(12);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void SearchApartments_Validparameters_ReturnsObjects()
        {
            // Arrange
            ApartmentViewDTO apartment1 = new ApartmentViewDTO()
            {
                Id = 12,
                Area = 21,
                BuildingAddress = "Baker St. 23",
                NumberOfRooms = 1
            };

            ApartmentViewDTO apartment2 = new ApartmentViewDTO()
            {
                Id = 25,
                Area = 44,
                BuildingAddress = "Marshall St. 11",
                NumberOfRooms = 2
            };

            var apartments = new List<ApartmentViewDTO>() { apartment1, apartment2 };

            ApartmentSearchDTO searchDto = new ApartmentSearchDTO()
            {
                AreaFrom = 1,
                AreaTo = 100,
            };

            var mockApartmentService = Substitute.For<IApartmentService>(); ;
            mockApartmentService.SearchByArea(searchDto.AreaFrom, searchDto.AreaTo).Returns(apartments);

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var actionResult = await controller.SearchApartments(searchDto) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var dtoResult = (List<ApartmentViewDTO>)actionResult.Value;

            for (int i = 0; i < dtoResult.Count; i++)
            {
                Assert.Equal(apartments[i], dtoResult[i]);
            }
        }

        [Fact]
        public async void SearchApartments_Invalidparameters_ThrowsInvalidAreaBadRequestException()
        {
            // Arrange
            ApartmentSearchDTO searchDto = new ApartmentSearchDTO()
            {
                AreaFrom = 100, // AreaFrom greater than AreaTo
                AreaTo = 1,
            };
            InvalidAreaBadRequestException mockException = new InvalidAreaBadRequestException();

            var mockApartmentService = Substitute.For<IApartmentService>(); ;

            var controller = new ApartmentsController(mockApartmentService);

            // Act
            var act = () => controller.SearchApartments(searchDto);

            // Assert
            InvalidAreaBadRequestException exception = 
                await Assert.ThrowsAsync<InvalidAreaBadRequestException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }
    }
}
