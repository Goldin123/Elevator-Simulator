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
    public class MoveToDestination : IMoveToDestination
    {
        private readonly ILogger<MoveToDestination> _logger;
        const string _twirl = "-\\|/";

        public MoveToDestination(ILogger<MoveToDestination> logger)
        {
            _logger = logger;
        }

        public async Task<Model.Building> MoveToDestinationAsync(Model.Elevator? closestElevator, int destinationFloor, Model.Building building)
        {
            try
            {
                if (closestElevator == null)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - Elevator is null."));
                    throw new ArgumentNullException(nameof(closestElevator));
                }

                if (destinationFloor <= 0 && destinationFloor > closestElevator.TopFloor)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - destination floor is out of range."));
                    throw new IndexOutOfRangeException(string.Format("{0} - {1}", DateTime.Now, nameof(destinationFloor)));
                }

                if (building == null)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - building is null."));
                    throw new IndexOutOfRangeException(string.Format("{0} - {1}", DateTime.Now, nameof(building)));

                }

                if (building?.Elevators?.Count > 0)
                {

                    foreach (var elevator in building.Elevators.Where(a => a.ElevatorID == closestElevator.ElevatorID))
                    {

                        elevator.Movement = destinationFloor > elevator.CurrentTravelFloor ? Model.Direction.Up : Model.Direction.Down; //This determines the direction on which the elevator needs to move.

                        string msg = string.Empty;

                        while (elevator.CurrentTravelFloor != destinationFloor && elevator.PassengerCount > 0)
                        {
                            await Move(elevator);

                            msg = string.Format("{0} - {1}", DateTime.Now, $"Elevator {elevator.ElevatorID} moving from floor {elevator.CurrentTravelFloor} to floor {elevator.DestinationFloor} and it is going ({elevator.Movement}), the speed of the current elevator is: {elevator.Speed}");
                            Console.WriteLine(msg);
                            // Prompt user to enter how many passengers to offload on each floor movement
                            if (elevator.Movement == Direction.Down)
                                elevator.CurrentTravelFloor--;
                            else
                                elevator.CurrentTravelFloor++;

                            Console.Write($"There's a total of {elevator.PassengerCount} passenger(s), enter how passenger(s) to get off at floor {elevator.CurrentTravelFloor}: ");
                            elevator.CurrentFloor = elevator.CurrentTravelFloor;
                            int offloadCount;
                            while (!int.TryParse(Console.ReadLine(), out offloadCount) || offloadCount < 0 || offloadCount > elevator.PassengerCount)
                            {
                                Console.Write($"Invalid input. Please enter a valid number (between 0 and {elevator.PassengerCount}): ");
                            }

                            elevator.PassengerCount -= offloadCount;

                            Console.WriteLine($"{offloadCount} passenger(s) offloaded at floor {elevator.CurrentFloor}. {elevator.PassengerCount} passenger(s) remaining.");
                            elevator.CurrentTravelFloor = elevator.CurrentTravelFloor;
                            elevator.CurrentFloor = elevator.CurrentTravelFloor;                           
                        }

                        elevator.CurrentFloor = destinationFloor;
                        elevator.CurrentTravelFloor = destinationFloor;
                        elevator.Movement = Model.Direction.Idle;
                    }

                }

                return building ?? new Building(1, 1, 1);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - {ex.Message}"));
                throw new Exception(ex.Message);
            }
        }

        private static async Task Move(Model.Elevator? elevator)
        {
            try
            {
                if (elevator != null)
                {
                    Console.WriteLine($"Elevator {elevator.ElevatorID} is moving {elevator.Movement}.");

                    WriteProgress(0);
                    for (var i = 0; i <= 100; ++i)
                    {
                        WriteProgress(i, true);
                        await Task.Delay(50 / (int)elevator.Speed);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static void WriteProgress(int progress, bool update = false)
        {
            if (update)
                Console.Write("\b");
            Console.Write(_twirl[progress % _twirl.Length]);
        }
    }
}
