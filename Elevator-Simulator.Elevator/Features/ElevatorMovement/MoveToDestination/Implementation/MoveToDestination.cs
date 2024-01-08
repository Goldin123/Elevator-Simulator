using Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Interface;
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

        public MoveToDestination() { }

        public async Task<bool> MoveToDestinationAsync(Model.Elevator elevator, int destinationFloor)
        {
            try
            {
                if (elevator == null)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(MoveToDestinationAsync)} - Elevator is null."));
                    throw new ArgumentNullException(nameof(elevator));
                }

                if (destinationFloor <= 0)
                {
                    _logger.LogCritical(string.Format("{0} - {1}", DateTime.Now, $"{nameof(destinationFloor)} - is out of range."));
                    throw new IndexOutOfRangeException(nameof(destinationFloor));
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
