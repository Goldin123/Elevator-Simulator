using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Simulator.Model
{
    public enum Direction
    {
        Idle,
        Up,
        Down
    }

    public enum ElevatorType 
    {
        Normal = 1,
        MediumSpeed = 2,
        HighSpeed = 3,
    }
}
