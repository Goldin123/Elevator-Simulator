using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Building.Features.BuildingManager.ConfigureBuilding.Interface
{
    public interface IConfigureBuilding
    {
        Task<Model.Building> ConfigureBuildingAsync();
    }
}
