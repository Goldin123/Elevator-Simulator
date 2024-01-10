using Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Interface;
using Elevator_Simulator.Building.Helpers;
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
                int currentFloor = Helper.GetIntInput(_logger,"Which Floor Are You Currently On? ");
                requestElevator.CurrentFloor = currentFloor;

                //Capture number of passenger
                int passengerCount = Helper.GetIntInput(_logger,"How Many Passengers Are There Waiting? ", maximumPassengers); 
                requestElevator.PassengerCount = passengerCount;

                //Capture destination floor
                int destinationFloor = Helper.GetIntInput(_logger, "What Is The Furthest Floor To Be Travelled? ", totalFloors, currentFloor);                
                requestElevator.DestinationFloor = destinationFloor;

                await Task.Delay(1);
                return requestElevator;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(RequestTheElevatorAsync)} - {ex.Message}"));
                throw new Exception(string.Format("{0} - {1}", DateTime.Now, ex.Message));
            }
        }

    }
}
