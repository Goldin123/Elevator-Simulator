using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.UnitTests.Features.ElevatorMovement
{
    public class MoveToDestinationTest
    {
        [Fact]
        public async void TestMoveToDestinationAsync()
        {
            try
            {
                var mock = new Mock<ILogger<MoveToDestination>>();
                ILogger<MoveToDestination> logger = mock.Object;

                var _moveToDestination = new MoveToDestination(logger);

                var closetElevator = new Model.Elevator(2, 5, 10, 9);

                var elevators = new List<Model.Elevator>
                {
                    new Model.Elevator(1,2,10,9),
                    new Model.Elevator(2,5,10,9),
                    new Model.Elevator(3,6,10,9),
                };
                int destinationFloor = 8;
                var building = new Model.Building(9, 3, 10);
                building.Elevators = elevators;

                var moveElevator = _moveToDestination.MoveToDestinationAsync(closetElevator,destinationFloor,building);

                Assert.NotNull(moveElevator);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
