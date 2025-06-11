using BuildingExample.Exceptions;
using BuildingExample.Models;
using BuildingExample.Repositories;
using BuildingExample.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace BuildingExampleTest.Services
{
    public class BuildingServiceTest
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

            var mockBuildingRepository = Substitute.For<IBuildingRepository>();
            mockBuildingRepository.GetOne(building.Id).Returns(building);
            var mockLogger = Substitute.For<ILogger<BuildingService>>();

            var service = new BuildingService(mockBuildingRepository, mockLogger);

            // Act
            var result = await service.GetOne(building.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(building, result);
        }

        [Fact]
        public async void GetOne_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);

            var mockBuildingRepository = Substitute.For<IBuildingRepository>();
            var mockLogger = Substitute.For <ILogger <BuildingService>>();

            var service = new BuildingService(mockBuildingRepository, mockLogger);

            // Act
            var act = () => service.GetOne(12);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void Update_ValidBuilding_ReturnsObject()
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

            var mockBuildingRepository = Substitute.For<IBuildingRepository>();
            var mockLogger = Substitute.For<ILogger<BuildingService>>();

            var service = new BuildingService(mockBuildingRepository, mockLogger);

            // Act
            var result = await service.Update(building.Id, building);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(building, result);
        }

        [Fact]
        public async void Update_InvalidData_ThrowsBadRequestException()
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
            BadRequestException mockException = new BadRequestException("Identifier value is invalid.");

            var mockBuildingRepository = Substitute.For<IBuildingRepository>();
            var mockLogger = Substitute.For<ILogger<BuildingService>>();

            var service = new BuildingService(mockBuildingRepository, mockLogger);

            // Act
            var act = () => service.Update(23, building);

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

            var mockBuildingRepository = Substitute.For<IBuildingRepository>();
            mockBuildingRepository.GetOne(building.Id).Returns(building);
            var mockLogger = Substitute.For<ILogger<BuildingService>>();

            var service = new BuildingService(mockBuildingRepository, mockLogger);

            // Act
            await service.Delete(building.Id);

            // Assert
            await mockBuildingRepository.Received().Delete(building); // check if function was called
        }

        [Fact]
        public async void Delete_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);

            var mockBuildingRepository = Substitute.For<IBuildingRepository>();
            var mockLogger = Substitute.For<ILogger<BuildingService>>();

            var service = new BuildingService(mockBuildingRepository, mockLogger);

            // Act
            var act = () => service.Delete(12);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }
    }
}
