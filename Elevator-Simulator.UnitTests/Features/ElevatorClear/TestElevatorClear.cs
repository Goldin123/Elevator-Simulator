using Elevator_Simulator.Elevator.Features.ElevatorClear.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.UnitTests.Features.ElevatorClearTest
{
    public class TestElevatorClear
    {
        [Fact]
        public async void TestClearAllPassengersAsync() 
        {
            try 
            {
                var mock = new Mock<ILogger<ClearElevator>>();
                ILogger<ClearElevator> logger = mock.Object;

                var _clearElevators = new ClearElevator(logger);

                var elevators = new List<Model.Elevator>
                {
                    new Model.Elevator(1,2,10,9),
                    new Model.Elevator(2,5,10,9),
                    new Model.Elevator(3,6,10,9),
                };

                var building = new Model.Building(9, 3, 10);
                building.Elevators = elevators;

                var clearPassengers = await _clearElevators.ClearAllPassengersAsync(building);

                Assert.Equal(0, building.Elevators[0].PassengerCount);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
