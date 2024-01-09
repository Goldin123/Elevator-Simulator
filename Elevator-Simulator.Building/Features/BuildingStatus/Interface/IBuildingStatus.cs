using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Building.Features.BuildingStatus.Interface
{
    public interface IBuildingStatus
    {
        Task<bool> DisplayBuildingStatusAsync(Model.Building building);
    }
}
