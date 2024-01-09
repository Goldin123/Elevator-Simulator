using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator_Simulator.Model;

namespace Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Interface
{
    public interface IAssignElevatorRequest
    {
        Task <Model.Building> AssignRequestAsync(Model.Elevator closestElevator, int currentFloor, int passengerCount, int destinationFloor, Model.Building building);
    }
}
