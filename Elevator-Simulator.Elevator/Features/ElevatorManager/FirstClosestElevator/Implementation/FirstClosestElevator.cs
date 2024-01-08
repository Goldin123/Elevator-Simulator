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
        private readonly List<Model.Elevator> _elevators;

        public FirstClosestElevator(ILogger<FirstClosestElevator> logger, List<Model.Elevator> elevators)
        {
            _logger = logger;
            _elevators = elevators;
        }

        public async Task<Model.Elevator> FindClosestElevatorAvailableAsync(int currentFloor)
        {
            int minDistance = int.MaxValue;
            Model.Elevator closestElevator = new Model.Elevator(0, 0, 0, 0);
            try
            {
                if (closestElevator != null && currentFloor > 0)
                {

                    foreach (var elevator in _elevators)
                    {
                        int.TryParse(elevator?.CurrentFloor.ToString(), out int tempCurrentFloor);
                        int distance = Math.Abs(tempCurrentFloor - currentFloor);
                        if (distance < minDistance)
                        {
                            _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - ElevatorID : {elevator?.ElevatorID} has min distance {distance}."));
                            minDistance = distance;
                            await Task.Delay(1);
                            closestElevator = elevator ?? new Model.Elevator(0, 0, 0, 0);
                        }
                    }
                    _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - ElevatorID : {closestElevator?.ElevatorID} is the closet available elevator."));
                    return closestElevator ?? new Model.Elevator(0, 0, 0, 0); ;
                }
                else
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - Current floor should be greater than zero."));
                    throw new ArgumentOutOfRangeException(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailableAsync)} - Current floor should be greater than zero."));
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
