using Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Interface;
using Elevator_Simulator.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Implementation
{
    public class MoveToDestination: IMoveToDestination
    {
        private readonly ILogger<MoveToDestination> _logger;

        public MoveToDestination(ILogger<MoveToDestination> logger) 
        {
            _logger = logger;
        }

        public async Task<bool> MoveToDestinationAsync(Model.Elevator? elevator, int destinationFloor)
        {
            try
            {
                if (elevator == null)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - Elevator is null."));
                    throw new ArgumentNullException(nameof(elevator));
                }

                if (destinationFloor <= 0 && destinationFloor > elevator.TopFloor)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - destination floor is out of range."));
                    throw new IndexOutOfRangeException(nameof(destinationFloor));
                }

                elevator.Movement = destinationFloor > elevator.CurrentFloor ? Model.Direction.Up : Model.Direction.Down; //This determines the direction on which the elevator needs to move.

                string msg = string.Empty;

                while (elevator.CurrentTravelFloor != destinationFloor)
                {
                    msg = string.Format("{0} - {1}", DateTime.Now, $"Elevator {elevator.ElevatorID} moving from floor {elevator.CurrentTravelFloor} to floor {elevator.CurrentTravelFloor + (elevator.Movement == Model.Direction.Up ? 1 : -1)} ({elevator.Movement}), the speed of this is: {elevator.Speed}");
                    _logger.LogInformation(msg);
                    Console.WriteLine(msg);
                    // Prompt user to enter how many passengers to offload on each floor movement
                    
                    Console.Write($"Enter the number of passengers to offload at floor {elevator.CurrentTravelFloor}: ");
                    int offloadCount;
                    while (!int.TryParse(Console.ReadLine(), out offloadCount) || offloadCount < 0 || offloadCount > elevator.PassengerCount)
                    {
                        Console.Write($"Invalid input. Please enter a valid number (between 0 and {elevator.PassengerCount}): ");
                    }

                    elevator.PassengerCount -= offloadCount;
                    Console.WriteLine($"{offloadCount} passengers offloaded. {elevator.PassengerCount} passengers remaining.");

                    await Task.Delay(1000/(int)elevator.Speed); // Simulate delay for real-time movement
                    elevator.CurrentTravelFloor += elevator.Movement == Model.Direction.Up ? 1 : -1;
                }

                elevator.CurrentFloor = destinationFloor;
                elevator.CurrentTravelFloor = destinationFloor;
                elevator.Movement = Model.Direction.Idle;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - {ex.Message}"));
                throw new Exception(ex.Message);
            }
        }
    }
}
