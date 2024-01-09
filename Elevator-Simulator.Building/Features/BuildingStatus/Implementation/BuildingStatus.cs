using Elevator_Simulator.Building.Features.BuildingStatus.Interface;
using Elevator_Simulator.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Building.Features.BuildingStatus.Implementation
{
    public class BuildingStatus : IBuildingStatus
    {
        private readonly ILogger<BuildingStatus> _logger;

        public BuildingStatus(ILogger<BuildingStatus> logger)
        {
            _logger = logger;
        }

        public async Task<bool> DisplayBuildingStatusAsync(Model.Building building) 
        {
            try
            {
                if (building !=null)
                {
                    string msg = string.Empty;
                    Console.WriteLine("\nBuilding Status:");
                    Console.WriteLine("----------------------------------------------");

                    msg = string.Format("{0} - {1}", DateTime.Now, $"Building total floors : {building.TotalFloors} \n" +
                                      $"Building numbers of elevators {building.Elevators?.Count()} \n " +
                                      $"Passenger maximum capacity: {building.MaximumPassengers}, ") ;
                                         
                        Console.WriteLine(msg);
                
                    Console.WriteLine("----------------------------------------------\n");
                    await Task.Delay(1);
                    return true;
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} - {1}", DateTime.Now, "No building information available."));
                    Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, "No building information available."));
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(DisplayBuildingStatusAsync)} - {ex.Message}"));
                throw new Exception(string.Format("{0} - {1}", DateTime.Now, ex.Message));

            }
        }

    }
}
