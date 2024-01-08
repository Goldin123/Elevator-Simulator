using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Interface
{
    public interface IMoveToDestination
    {
        Task<bool> MoveToDestinationAsync(Model.Elevator elevator, int destinationFloor);
    }
}
