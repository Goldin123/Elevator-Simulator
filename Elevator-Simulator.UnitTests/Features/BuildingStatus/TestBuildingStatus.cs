using Elevator_Simulator.Building.Features.BuildingStatus.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.UnitTests.Features.BuildingStatusTest
{
    public class TestBuildingStatus
    {
        [Fact]
        public async void TestDisplayBuildingStatusAsync()
        {
            try
            {
                var mock = new Mock<ILogger<BuildingStatus>>();
                ILogger<BuildingStatus> logger = mock.Object;

                var _buildStatus = new BuildingStatus(logger);

                var elevators = new List<Model.Elevator>
                {
                    new Model.Elevator(1,2,10,9),
                    new Model.Elevator(2,5,10,9),
                    new Model.Elevator(3,6,10,9),
                };

                var building = new Model.Building(9, 3, 10);
                building.Elevators = elevators;

                var displayBuild = await _buildStatus.DisplayBuildingStatusAsync(building);

                Assert.True(displayBuild);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
