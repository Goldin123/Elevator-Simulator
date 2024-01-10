using Elevator_Simulator.Elevator.Features.ElevatorStatus.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.UnitTests.Features.ElevatorStatusTest
{
    public class TestElevatorStatus
    {
        [Fact]
        public async void TestDisplayElevatorStatusAsync() 
        {
            try
            {
                var mock = new Mock<ILogger<ElevatorStatus>>();
                ILogger<ElevatorStatus> logger = mock.Object;

                var _elevatorStatus = new ElevatorStatus(logger);

                var elevators = new List<Model.Elevator>
                {
                    new Model.Elevator(1,2,10,9),
                    new Model.Elevator(2,5,10,9),
                    new Model.Elevator(3,6,10,9),
                };

                var elevatorStatus = await _elevatorStatus.DisplayElevatorStatusAsync(elevators);

                Assert.True(elevatorStatus);

            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
