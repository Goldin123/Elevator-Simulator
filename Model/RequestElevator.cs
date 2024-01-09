using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Model
{
    /// <summary>
    /// These are primary inputs needed to request a elevator.
    /// </summary>
    public class RequestElevator
    {
        /// <summary>
        /// The current floor you are in when requesting an elevator. 
        /// </summary>
        public int? CurrentFloor { get; set; }
        /// <summary>
        /// The total number of passengers awaiting to board the elevator.
        /// </summary>
        public int? PassengerCount { get; set; }
        /// <summary>
        /// The maximum floor the elevator should travel on that request, although in practical this is not needed.
        /// </summary>
        public int? DestinationFloor { get; set; }

        public RequestElevator(int currentFloor, int passengerCount, int destinationFloor) 
        {
            CurrentFloor = currentFloor;
            PassengerCount = passengerCount;
            DestinationFloor = destinationFloor;
        }
    }
}
