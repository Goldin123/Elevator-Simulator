using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorClear.Interface
{
    public interface IClearElevator
    {
        Task<Model.Building> ClearAllPassengersAsync(Model.Building building);
    }
}
