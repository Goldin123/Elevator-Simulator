using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorStatus.Interface
{
    public interface IElevatorStatus
    {
        Task<bool> DisplayElevatorStatus(List<Model.Elevator> elevators);
    }
}
