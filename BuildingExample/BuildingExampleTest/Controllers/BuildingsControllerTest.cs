using BuildingExample.Controllers;
using BuildingExample.Exceptions;
using BuildingExample.Models;
using BuildingExample.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BuildingExampleTest.Controllers
{
    public class BuildingsControllerTest
    {
        [Fact]
        public async void GetBuildings_Success_ReturnsObjects()
        {
            // Arrange
            Building building1 = new Building()
            {
                Id = 12,
                Address = "Baker St. 23",
                HasElevator = true,
                Floors = 2,
                YearOfConstruction = 2023
            };

            Building building2 = new Building()
            {
                Id = 14,
                Address = "Marshall St. 23",
                HasElevator = false,
                Floors = 3,
                YearOfConstruction = 1989
            };

            var buildings = new List<Building>() { building1, building2 };

            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.GetAll().Returns(buildings);

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var actionResult = await controller.GetBuildings() as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var result = (List<Building>)actionResult.Value;

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(buildings[i], result[i]);
            }
        }

        [Fact]
        public async void GetBuilding_ValidId_ReturnsObject()
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

            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.GetOne(1).Returns(building);

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var actionResult = await controller.GetBuilding(1) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var result = (Building)actionResult.Value;
            Assert.Equal(building, result);
        }

        [Fact]
        public async void GetBuilding_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);
            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.When(x => x.GetOne(12)).Do(x => { throw mockException; });

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var act = () => controller.GetBuilding(12);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void PostBuilding_ValidBuilding_SetsLocationHeader()
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

            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.Add(building).Returns(building);

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var actionResult = await controller.PostBuilding(building) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal("GetBuilding", actionResult.ActionName);
            Assert.Equal(building.Id, actionResult.RouteValues["id"]);
            Assert.Equal(building, actionResult.Value);
        }

        [Fact]
        public async void PutBuilding_ValidBuilding_ReturnsObject()
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

            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.Update(building.Id, building).Returns(building);

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var actionResult = await controller.PutBuilding(building.Id, building) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            var result = (Building)actionResult.Value;
            Assert.Equal(building, result);
        }

        [Fact]
        public async void PutBuilding_InvalidData_ThrowsBadRequestException()
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

            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.When(x => x.Update(23, building)).Do(x => { throw mockException; });

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var act = () => controller.PutBuilding(23, building);

            // Assert
            BadRequestException exception = await Assert.ThrowsAsync<BadRequestException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }

        [Fact]
        public async void DeleteBuilding_ValidId_ReturnsNoContent()
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

            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.GetOne(1).Returns(building);

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var actionResult = await controller.DeleteBuilding(1) as NoContentResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public async void DeleteBuilding_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            NotFoundException mockException = new NotFoundException(12);
            var mockBuildingService = Substitute.For<IBuildingService>(); ;
            mockBuildingService.When(x => x.Delete(12)).Do(x => { throw mockException; });

            var controller = new BuildingsController(mockBuildingService);

            // Act
            var act = () => controller.DeleteBuilding(12);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(mockException.Message, exception.Message);
        }
    }
}
