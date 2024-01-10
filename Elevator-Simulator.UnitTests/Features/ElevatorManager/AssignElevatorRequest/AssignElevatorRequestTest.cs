using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Implementation;

namespace Elevator_Simulator.UnitTests.Features.ElevatorManager.AssignElevatorRequestTest
{
    public class AssignElevatorRequestTest
    {

        [Fact]
        public async void TestAssignElevatorAsync()
        {
            try
            {
                var mock = new Mock<ILogger<AssignElevatorRequest>>();
                ILogger<AssignElevatorRequest> logger = mock.Object;

                var _assignElevatorRequest = new AssignElevatorRequest(logger);

                int currentFloor = 1;
                int passengerCount = 10;
                int destinationFloor = 2;
                int maxCapacity = 10;
                int topFloor = 10;
                int totalElevators = 3;
                var elevator = new Model.Elevator(1, currentFloor, maxCapacity, topFloor);
                var building = new Model.Building(topFloor, totalElevators, maxCapacity);

                var testAssignelevator = await _assignElevatorRequest.AssignRequestAsync(elevator, currentFloor, passengerCount, destinationFloor, building);

                Assert.NotNull(testAssignelevator);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);    
            }

        }
    }
}
