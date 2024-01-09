using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Interface
{
    public interface IFirstClosestElevator
    {
        Task<Model.Elevator> FindClosestElevatorAvailableAsync(List<Model.Elevator> elevators, int currentFloor);
    }
}
