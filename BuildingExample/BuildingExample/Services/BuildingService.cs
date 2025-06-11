using BuildingExample.Exceptions;
using BuildingExample.Models;
using BuildingExample.Repositories;

namespace BuildingExample.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly ILogger<BuildingService> _logger;

        public BuildingService(IBuildingRepository buildingRepository, ILogger<BuildingService> logger)
        {
            _buildingRepository = buildingRepository;
            _logger = logger;
        }

        public async Task<Building> Add(Building building)
        {
            await _buildingRepository.Add(building);
            return building;
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"Check if building with id {id} exists.");
            var building = await _buildingRepository.GetOne(id);

            if (building == null)
            {
                _logger.LogError($"Building with id {id} does not exist.");
                throw new NotFoundException(id);
            }

            await _buildingRepository.Delete(building);
            _logger.LogInformation($"Building with id {id} is deleted.");
        }

        public async Task<List<Building>> GetAll()
        {
            return await _buildingRepository.GetAll();
        }

        public async Task<Building> GetOne(int id)
        {
            var building = await _buildingRepository.GetOne(id);
            if (building == null)
            {
                throw new NotFoundException(id);
            }
            return building;
        }

        public async Task<Building> Update(int id, Building building)
        {
            if (id != building.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            await _buildingRepository.Update(building);
            return building;
        }
    }
}
