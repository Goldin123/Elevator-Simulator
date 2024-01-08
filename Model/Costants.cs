using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Model
{
    public static class Notifications
    {
        public static string? Successful = "Success";
        public static string? ElevatorInMotion = "Elevator {0} is currently in motion. Request assigned to it will be processed after it reaches its destination.";
    }
}
