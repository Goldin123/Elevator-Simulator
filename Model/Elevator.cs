using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Model
{
    /// <summary>
    /// This defines the core structure of an elevator.
    /// </summary>
    public class Elevator
    {
        /// <summary>
        /// UniqueID which identifies the elevator.
        /// </summary>
        public int? ElevatorID { get; }
        /// <summary>
        /// This is for tracking purposes to know where exactly is the elevator at any given point.
        /// </summary>
        public int? CurrentFloor { get; set; }
        /// <summary>
        /// Determines the current passenger count in the elevator.
        /// </summary>
        public int? PassengerCount { get; set; }
        /// <summary>
        /// In practical this is known once the passengers are in the elevator, but for the simulation, this is should be entered before you board. 
        /// </summary>
        public int? DestinationFloor { get; set; }
        /// <summary>
        /// Only the following movements are allowed for an elevator.
        /// </summary>
        public Direction? Movement { get; set; }
        /// <summary>
        /// This assists with off-loading passengers.
        /// </summary>
        public int? CurrentTravelFloor { get; set; }
        /// <summary>
        /// This is the maximum passenger capacity. 
        /// </summary>
        public int? MaxCapacity { get; }
        /// <summary>
        /// This holds the maximum floor level on which the elevator can travel to. 
        /// </summary>
        public int? TopFloor { get; set; }

        /// <summary>
        /// The velocity of the speed which is randomly assigned when the elevator is created. 
        /// </summary>
        public ElevatorType Speed { get; set; }

        /// <summary>
        /// This will control the accessibility of the elevator depending if we need to do physical maintenance, this can be switched on/off
        /// </summary>
        public bool? IsUnderMaintenance { get; set; }
        /// <summary>
        /// Basically for an elevator to function, the following parameters needs to be passed through.
        /// </summary>
        /// <param name="elevatorID"></param>
        /// <param name="currentFloor"></param>
        /// <param name="maxCapacity"></param>
        public Elevator(int elevatorID, int currentFloor, int maxCapacity,int topFloor)
        {
            Random rnd = new Random();
            int elevatorType = rnd.Next(1, 4); 
            ElevatorID = elevatorID;
            CurrentFloor = currentFloor;
            PassengerCount = 0;
            DestinationFloor = currentFloor;
            Movement = Direction.Idle;
            CurrentTravelFloor = currentFloor;
            MaxCapacity = maxCapacity;
            TopFloor = topFloor;
            Speed = (ElevatorType)elevatorType;
            IsUnderMaintenance = false;
        }


    }
}
