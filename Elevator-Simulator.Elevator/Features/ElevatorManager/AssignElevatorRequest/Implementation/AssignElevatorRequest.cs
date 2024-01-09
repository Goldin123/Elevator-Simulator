using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Implementation
{
    public class AssignElevatorRequest : IAssignElevatorRequest
    {
        private readonly ILogger<AssignElevatorRequest> _logger;

        public AssignElevatorRequest(ILogger<AssignElevatorRequest> logger)
        {
            _logger = logger;
        }

        public async Task<Model.Building> AssignRequestAsync(Model.Elevator closestElevator, int currentFloor, int passengerCount, int destinationFloor, Model.Building building)
        {
            try
            {
                if (closestElevator == null)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(AssignRequestAsync)} - Elevator is null."));
                    throw new ArgumentNullException(nameof(closestElevator));
                }

                if (currentFloor <= 0)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(currentFloor)} - is out of range."));
                    throw new IndexOutOfRangeException(nameof(currentFloor));
                }

                if (passengerCount <= 0)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(passengerCount)} - is out of range."));
                    throw new IndexOutOfRangeException(nameof(passengerCount));
                }

                if (destinationFloor <= 0)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(destinationFloor)} - is out of range."));
                    throw new IndexOutOfRangeException(nameof(destinationFloor));
                }


                if (closestElevator.Movement == Model.Direction.Idle)
                {
                    _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(AssignRequestAsync)} - ElevatorID : {closestElevator.ElevatorID} is on idle."));
                    closestElevator.PassengerCount = passengerCount;
                    closestElevator.DestinationFloor = destinationFloor;
                    closestElevator.CurrentTravelFloor = currentFloor;

                    if (building.Elevators?.Count > 0)
                    {
                        foreach (var itm in building.Elevators.Where(a => a.ElevatorID == closestElevator.ElevatorID))
                        {
                            itm.PassengerCount = closestElevator.PassengerCount;
                            itm.DestinationFloor = closestElevator.DestinationFloor;
                            itm.CurrentTravelFloor = closestElevator.CurrentTravelFloor;
                        }
                    }

                    await Task.Delay(1);
                    return building;
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} - {1}", DateTime.Now, string.Format(Model.Notifications.ElevatorInMotion ?? "", closestElevator.ElevatorID)));
                    Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, string.Format(Model.Notifications.ElevatorInMotion ?? "", closestElevator.ElevatorID)));
                    return building;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(AssignRequestAsync)} - {ex.Message}"));
                throw new Exception(ex.Message);

            }
        }
    }
}
