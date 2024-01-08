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
        Task <bool> AssignRequestAsync(Model.Elevator elevator, int currentFloor, int passengerCount, int destinationFloor);
    }
}
