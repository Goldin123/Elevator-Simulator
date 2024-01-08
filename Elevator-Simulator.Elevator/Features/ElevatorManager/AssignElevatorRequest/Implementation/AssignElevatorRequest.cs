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

        public async Task<bool> AssignRequest(Model.Elevator elevator, int currentFloor, int passengerCount, int destinationFloor)
        {
            try 
            {
                if (elevator.Movement == Model.Direction.Idle)
                {
                    _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"{nameof(AssignRequest)} - ElevatorID : {elevator.ElevatorID} is on idle."));
                    elevator.CurrentFloor = currentFloor;
                    elevator.PassengerCount = passengerCount;
                    elevator.DestinationFloor = destinationFloor;
                    elevator.CurrentTravelFloor = currentFloor;
                    return true;
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} - {1}", DateTime.Now, string.Format(Model.Notifications.ElevatorInMotion ?? "", elevator.ElevatorID)));
                    Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, string.Format(Model.Notifications.ElevatorInMotion ?? "", elevator.ElevatorID)));
                    return false;
                }
            }
            catch (Exception ex) 
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(AssignRequest)} - {ex.Message}"));
                return false;
            }
        }
    }
}
