using System;
using Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Implementation;
using Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Interface;
using Elevator_Simulator.Building.Features.BuildingManager.ConfigureBuilding.Implementation;
using Elevator_Simulator.Building.Features.BuildingManager.ConfigureBuilding.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorStatus.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorStatus.Interface;
using Elevator_Simulator.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elevator_Simulator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                ServiceProvider _services;
                IRequestTheElevator _buildingManagerCaptureUserRequestService;
                IConfigureBuilding _buildingManagerConfigureBuildingService;
                IFirstClosestElevator _elevatorManagerFirstClosestElevatorService;
                IAssignElevatorRequest _elevatorManagerAssignElevatorRequestService;
                IMoveToDestination _elevatorMovementToDestinationService;
                IElevatorStatus _elevatorStatusService;
                List<Model.Elevator> _tempElevators = new List<Model.Elevator>();

                //Setup the application dependencies
                SetupServices(out _services, out _buildingManagerCaptureUserRequestService, out _buildingManagerConfigureBuildingService, out _elevatorManagerFirstClosestElevatorService, out _elevatorManagerAssignElevatorRequestService, out _elevatorMovementToDestinationService, out _elevatorStatusService);

                var _logger = _services.GetRequiredService<ILogger<Program>>();

                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, "Application starting....."));

                //Configure the building
                var building = await _buildingManagerConfigureBuildingService.ConfigureBuildingAsync();

                if (building == null)
                {
                    throw new Exception(string.Format("{0} - {1}", DateTime.Now, "Application had problems configuring the building."));
                }

                _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, "Building is now all setup. Now let's get passengers onto the elevators so they can be safely delivered to their desired destination."));

                string? userInput = string.Empty;
                do
                {
                    Console.WriteLine("\nPlease enter details for a new request (type 'exit' to quit):");

                    //Request an elevator

                    var elevatorRequest = await _buildingManagerCaptureUserRequestService.RequestTheElevatorAsync(building.MaximumPassengers ?? 0, building.TotalFloors ?? 0);

                    //Find closet elevator

                    if (elevatorRequest == null)
                        throw new Exception(string.Format("{0} - {1}", DateTime.Now, "Application had problems receiving user elevator requests."));

                    var findClosetElevator = await _elevatorManagerFirstClosestElevatorService.FindClosestElevatorAvailableAsync(building.Elevators ?? _tempElevators, elevatorRequest.CurrentFloor ?? 0);

                    if (findClosetElevator == null)
                        Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, $"No elevators available for floor {elevatorRequest.CurrentFloor}."));
                    else
                    {
                        //Assign Request to elevator

                        var assignElevator = await _elevatorManagerAssignElevatorRequestService.AssignRequestAsync(building.Elevators ?? _tempElevators, findClosetElevator, elevatorRequest.CurrentFloor ?? 0, elevatorRequest.PassengerCount ?? 0, elevatorRequest.DestinationFloor ?? 0);

                        if (assignElevator)
                        {
                            //Move Elevator to destination
                            var moveElevator = await _elevatorMovementToDestinationService.MoveToDestinationAsync(building.Elevators?.Where(a => a.ElevatorID == findClosetElevator.ElevatorID).FirstOrDefault(), elevatorRequest.DestinationFloor ?? 0);
                        }
                    }

                    //Display elevator status

                    await _elevatorStatusService.DisplayElevatorStatusAsync(building.Elevators ?? _tempElevators);

                    Console.WriteLine("\nType 'exit' to quit or press Enter to continue.");
                    userInput = Console.ReadLine();

                } while (userInput != "exit");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, $"{nameof(Main)} - {ex.Message}"));
            }
        }
        private static void SetupServices(out ServiceProvider _services, out IRequestTheElevator _buildingManagerCaptureUserRequestService, out IConfigureBuilding _buildingManagerConfigureBuildingService, out IFirstClosestElevator _elevatorManagerFirstClosestElevatorService, out IAssignElevatorRequest _elevatorManagerAssignElevatorRequestService, out IMoveToDestination _elevatorMovementToDestinationService, out IElevatorStatus _elevatorStatusService)
        {
            _services = CreateServices();
            _elevatorManagerAssignElevatorRequestService = _services.GetRequiredService<IAssignElevatorRequest>();
            _elevatorManagerFirstClosestElevatorService = _services.GetRequiredService<IFirstClosestElevator>();
            _elevatorMovementToDestinationService = _services.GetRequiredService<IMoveToDestination>();
            _elevatorStatusService = _services.GetRequiredService<IElevatorStatus>();
            _buildingManagerCaptureUserRequestService = _services.GetRequiredService<IRequestTheElevator>();
            _buildingManagerConfigureBuildingService = _services.GetRequiredService<IConfigureBuilding>();
        }

        static ServiceProvider CreateServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(options =>
                {
                    options.ClearProviders();
                    options.AddConsole();
                })

                .AddSingleton<IAssignElevatorRequest, AssignElevatorRequest>()
                .AddSingleton<IFirstClosestElevator, FirstClosestElevator>()
                .AddSingleton<IMoveToDestination, MoveToDestination>()
                .AddSingleton<IElevatorStatus, ElevatorStatus>()
                .AddSingleton<IRequestTheElevator, RequestTheElevator>()
                .AddSingleton<IConfigureBuilding, ConfigureBuilding>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}