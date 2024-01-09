using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Model
{
    /// <summary>
    /// This holds the core object that defines a building
    /// </summary>
    public class Building
    {
        /// <summary>
        /// The number of floors in the building.
        /// </summary>
        public int? TotalFloors { get; set; }
        /// <summary>
        /// The total number of elevators in the building.
        /// </summary>
        public int? TotalElevators { get; set; }
        /// <summary>
        /// The allowed maximum passenger capacity the elevator has to carry for the building.
        /// </summary>
        public int? MaximumPassengers { get; set; }
        /// <summary>
        /// The actual elevators themselves.
        /// </summary>
        public List<Elevator>? Elevators  { get; set; }

        public Building(int totalFloors, int totalElevators, int maximumPassengers) 
        {
            if (totalFloors <= 0)
                throw new ArgumentOutOfRangeException("Totals floor should be greater than zero.");

            if (totalElevators <= 0)
                throw new ArgumentOutOfRangeException("Totals elevators should be greater than zero.");

            if(maximumPassengers <= 0)
                throw new ArgumentOutOfRangeException("Maximum passengers should be greater than zero.");

            TotalFloors = totalFloors;
            TotalElevators = totalElevators;
            MaximumPassengers = maximumPassengers;
            Elevators = new List<Elevator>();   
        }
    }
}
