using Elevator_Simulator.Building.Features.BuildingManager.ConfigureBuilding.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Building.Features.BuildingManager.ConfigureBuilding.Implementation
{
    public class ConfigureBuilding : IConfigureBuilding
    {
        private readonly ILogger<ConfigureBuilding> _logger;

        public ConfigureBuilding(ILogger<ConfigureBuilding> logger)
        {
            _logger = logger;
        }

        public async Task<Model.Building> ConfigureBuildingAsync()
        {
            Model.Building building = new Model.Building(0, 0, 0);
            try
            {
                Console.WriteLine("Welcome to Goldin's Elevator manager, let's first get started by configuring the building. Follow the prompt below.");
                Console.WriteLine("Enter the number of floors in the building:");
                int.TryParse(Console.ReadLine(), out int totalFloors);
                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"The building has a total of {totalFloors} floor(s)."));

                Console.WriteLine("Enter the number of elevators in the building:");
                int.TryParse(Console.ReadLine(), out int elevatorCount);
                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"The building has a total of {elevatorCount} elevators(s)."));

                Console.WriteLine("Enter the maximum capacity of each elevator:");
                int.TryParse(Console.ReadLine(), out int maxCapacity);
                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"Each elevator can carry a maximum of {maxCapacity} passenger(s)."));

                List<Model.Elevator> elevators = new List<Model.Elevator>();
                for (int i = 1; i <= elevatorCount; i++)
                {
                    elevators.Add(new Model.Elevator(i, 1, maxCapacity, totalFloors)); // All elevators start at the first floor
                }
                building = new Model.Building(totalFloors, elevatorCount, maxCapacity);
                building.Elevators = elevators;
                await Task.Delay(1);
                return building;

            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(ConfigureBuildingAsync)} - {ex.Message}"));
                throw new Exception(string.Format("{0} - {1}", DateTime.Now, ex.Message));
            }
        }
    }
}
