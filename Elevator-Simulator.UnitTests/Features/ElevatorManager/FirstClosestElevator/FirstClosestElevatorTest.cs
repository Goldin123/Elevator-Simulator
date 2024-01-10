using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.UnitTests.Features.ElevatorManager.FirstClosestElevator
{
    public class FirstClosestElevatorTest
    {
        [Fact]
        public async void TestFindClosestElevatorAvailableAsync()
        {
            try
            {
                var mock = new Mock<ILogger<Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation.FirstClosestElevator>>();
                ILogger<Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation.FirstClosestElevator> logger = mock.Object;

                var _firstClosestElevator = new Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation.FirstClosestElevator(logger);

                List<Model.Elevator> elevators = new List<Model.Elevator>
                {
                    new Model.Elevator(1,2,10,9),
                    new Model.Elevator(2,5,10,9),
                    new Model.Elevator(3,6,10,9),
                };
                int currentFloor = 2;
                int capacity = 10;

                var closetElevator = await _firstClosestElevator.FindClosestElevatorAvailableAsync(elevators, currentFloor, capacity);

                Assert.NotNull(closetElevator);
            }
            catch(Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
