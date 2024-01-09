using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Interface
{
    public interface IRequestTheElevator
    {
        Task<Model.RequestElevator> RequestTheElevatorAsync(int maximumPassengers, int totalFloors);
    }
}
