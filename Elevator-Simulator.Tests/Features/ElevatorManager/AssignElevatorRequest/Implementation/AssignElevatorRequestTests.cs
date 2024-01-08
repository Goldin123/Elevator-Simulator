using NUnit.Framework;
using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.WebSockets;

namespace Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Implementation.Tests
{
    [TestFixture()]
    public class AssignElevatorRequestTests
    {
        [Test()]
        public  void AssignRequestTest()
        {
            try
            {
                var mock = new Mock<ILogger<AssignElevatorRequest>>();
                ILogger<AssignElevatorRequest> logger = mock.Object;
                IAssignElevatorRequest _assignElevatorRequest = new AssignElevatorRequest(logger);

                int currentFloor = 1;
                int passengerCount = 10;
                int destinationFloor = 2;
                int maxCapacity = 10;
                int topFloor = 10;
                var elevator = new Model.Elevator(1, currentFloor, maxCapacity, topFloor);

                var testAssignElevator =  _assignElevatorRequest.AssignRequestAsync(elevator, currentFloor, passengerCount, destinationFloor);

                Assert.Equals(true, testAssignElevator);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

    }
}