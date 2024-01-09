using Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Implementation
{
    public class RequestTheElevator : IRequestTheElevator
    {
        private readonly ILogger<RequestTheElevator> _logger;

        public RequestTheElevator(ILogger<RequestTheElevator> logger)
        {
            _logger = logger;
        }
        public async Task<Model.RequestElevator> RequestTheElevatorAsync(int maximumPassengers, int totalFloors)
        {
            Model.RequestElevator requestElevator = new Model.RequestElevator(0, 0, 0);
            try
            {
                //Capture current floor
                int currentFloor = GetInput("Current floor: ");
                requestElevator.CurrentFloor = currentFloor;
                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"You are currently requesting an elevator on floor {currentFloor}."));

                //Capture number of passenger
                int passengerCount = GetInput("Number of passengers: ", maximumPassengers); 
                requestElevator.PassengerCount = passengerCount;
                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"The number of passengers are {passengerCount}."));

                //Capture destination floor
                int destinationFloor = GetInput("Destination floor: ", totalFloors, currentFloor);                
                requestElevator.DestinationFloor = destinationFloor;


                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, $"The last passenger(s) are destined  for floor {destinationFloor}."));
                await Task.Delay(1);
                return requestElevator;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(RequestTheElevatorAsync)} - {ex.Message}"));
                throw new Exception(string.Format("{0} - {1}", DateTime.Now, ex.Message));
            }
        }


        int GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            int userInput;
            while (!(int.TryParse(Console.ReadLine(), out userInput) && userInput > 0))
            {
                Console.WriteLine("Please enter a positive number.");
            }
            return userInput;
        }
        int GetInput(string prompt,int maximum)
        {
            Console.WriteLine(prompt);
            int userInput;
            while (!(int.TryParse(Console.ReadLine(), out userInput) && userInput > 0 && userInput <= maximum))
            {
                Console.WriteLine($"Please enter a positive number which is between 1 and {maximum}.");
            }
            return userInput;
        }
        int GetInput(string prompt, int maximum,int current)
        {
            Console.WriteLine(prompt);
            int userInput;
            while (!(int.TryParse(Console.ReadLine(), out userInput) && userInput > 0 && userInput <= maximum && userInput != current ))
            {
                Console.WriteLine($"Please enter a positive number which is between 1 and {maximum} and should not be {current}.");
            }
            return userInput;
        }
    }
}
