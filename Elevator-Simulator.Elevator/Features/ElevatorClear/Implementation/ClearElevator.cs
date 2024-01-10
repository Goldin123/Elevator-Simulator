using Elevator_Simulator.Elevator.Features.ElevatorClear.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorClear.Implementation
{
    public class ClearElevator : IClearElevator
    {
        private readonly ILogger<ClearElevator> _logger;

        public ClearElevator(ILogger<ClearElevator> logger) 
        {
            _logger = logger;
        }

        public async Task<Model.Building> ClearAllPassengersAsync(Model.Building building) 
        {
            try 
            {
                Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, $"{nameof(ClearAllPassengersAsync)} - attempting to clear all passengers."));

                foreach(var elevator in building.Elevators) 
                {
                    if (elevator.PassengerCount > 0)
                    {
                        Console.WriteLine($"{elevator.PassengerCount} passenger(s) off loaded on floor {elevator.CurrentFloor}. ");
                        elevator.PassengerCount = 0;
                    }
                }
                await Task.Delay(1);
                return building;
            }
            catch (Exception ex) 
            {
                throw new Exception(string.Format("{0} - {1}", DateTime.Now, ex.Message));

            }
        }
    }
}
