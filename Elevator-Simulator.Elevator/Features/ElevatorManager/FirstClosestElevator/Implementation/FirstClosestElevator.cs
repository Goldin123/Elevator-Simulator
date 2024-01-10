using Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Interface;
using Elevator_Simulator.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation
{
    public class FirstClosestElevator : IFirstClosestElevator
    {
        private readonly ILogger<FirstClosestElevator> _logger;

        public FirstClosestElevator(ILogger<FirstClosestElevator> logger)
        {
            _logger = logger;
        }

        public async Task<Model.Elevator>? FindClosestElevatorAvailableAsync(List<Model.Elevator> elevators, int currentFloor, int capacity)
        {
            int minDistance = int.MaxValue;
            Model.Elevator? closestElevator = null;
            try
            {
                if (elevators.Count > 0)
                {

                    if (currentFloor > 0)
                    {

                        foreach (var elevator in elevators.Where(a => (a.PassengerCount + capacity) <= a.MaxCapacity && a.IsUnderMaintenance == false))
                        {
                            int.TryParse(elevator?.CurrentFloor.ToString(), out int tempCurrentFloor);
                            int distance = Math.Abs(tempCurrentFloor - currentFloor);
                            if (distance < minDistance)
                            {
                                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - ElevatorID : {elevator?.ElevatorID} has a minimum distance {distance} floor(s)."));
                                minDistance = distance;

                                closestElevator = elevator;
                            }
                        }
                        if (closestElevator != null)
                            _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - ElevatorID : {closestElevator?.ElevatorID} is the closet available elevator. Please note the speed of this elevator is {closestElevator?.Speed}"));

                        await Task.Delay(1);
                        return closestElevator;
                    }
                    else
                    {
                        _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - Current floor should be greater than zero."));
                        throw new ArgumentOutOfRangeException(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - Current floor should be greater than zero."));
                    }
                }
                else
                {
                    return closestElevator;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - {ex.Message}"));
                throw new Exception(ex.Message);

            }
        }

    }
}
