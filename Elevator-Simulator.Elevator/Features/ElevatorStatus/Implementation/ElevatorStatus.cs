using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorStatus.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorStatus.Implementation
{
    public class ElevatorStatus : IElevatorStatus
    {
        private readonly ILogger<ElevatorStatus> _logger;

        public ElevatorStatus(ILogger<ElevatorStatus> logger) 
        {
            _logger = logger;
        }
        public async Task<bool> DisplayElevatorStatusAsync(List<Model.Elevator> elevators)
        {
            try
            {
                if (elevators?.Count > 0)
                {
                    string msg = string.Empty;
                    Console.WriteLine($"\nElevator Status as at :{DateTime.Now}");
                    Console.WriteLine("----------------------------------------------");    
                    foreach (var elevator in elevators)
                    {
                        msg =$"Elevator {elevator.ElevatorID} which has speed {elevator.Speed} at floor {elevator.CurrentFloor}, " +
                                          $"Passenger count: {elevator.PassengerCount}, " +
                                          $"Destination floor: {elevator.DestinationFloor}, " +
                                          $"Direction: {elevator.Movement}";
                        Console.WriteLine(msg);
                    }
                    Console.WriteLine("----------------------------------------------");
                    await Task.Delay(1);
                    return true;
                }
                else 
                {
                    _logger.LogWarning(string.Format("{0} - {1}", DateTime.Now, "No elevators available."));
                    Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, "No elevators available."));
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(DisplayElevatorStatusAsync)} - {ex.Message}"));
                throw new Exception(string.Format("{0} - {1}", DateTime.Now, ex.Message));

            }
        }

    }
}
