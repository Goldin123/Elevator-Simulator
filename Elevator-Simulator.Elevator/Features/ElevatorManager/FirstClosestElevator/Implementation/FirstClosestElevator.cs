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

        public async Task<Model.Elevator> FindClosestElevatorAvailable(int currentFloor)
        {
            int minDistance = int.MaxValue;
            Model.Elevator closestElevator = new Model.Elevator(0, 0, 0, 0);
            try
            {
                foreach (var elevator in _elevators)
                {
                    int.TryParse(elevator?.CurrentFloor.ToString(), out int tempCurrentFloor);
                    int distance = Math.Abs(tempCurrentFloor - currentFloor);
                    if (distance < minDistance)
                    {
                        _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailable)} - ElevatorID : {elevator?.ElevatorID} has min distance {distance}."));
                        minDistance = distance;
                        closestElevator = elevator ?? new Model.Elevator(0, 0, 0, 0);
                    }
                }
                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailable)} - ElevatorID : {closestElevator?.ElevatorID} is the closet available elevator."));
                return closestElevator ?? new Model.Elevator(0, 0, 0, 0); ;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(FindClosestElevatorAvailable)} - {ex.Message}"));
                return closestElevator;
            }
        }

    }
}
