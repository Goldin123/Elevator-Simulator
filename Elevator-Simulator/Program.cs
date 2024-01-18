using System;
using Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Implementation;
using Elevator_Simulator.Building.Features.BuildingManager.CaptureUserRequest.Interface;
using Elevator_Simulator.Building.Features.BuildingManager.ConfigureBuilding.Implementation;
using Elevator_Simulator.Building.Features.BuildingManager.ConfigureBuilding.Interface;
using Elevator_Simulator.Building.Features.BuildingStatus.Implementation;
using Elevator_Simulator.Building.Features.BuildingStatus.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorClear.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorClear.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorManager.AssignElevatorRequest.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorManager.FirstClosestElevator.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorMovement.MoveToDestination.Interface;
using Elevator_Simulator.Elevator.Features.ElevatorStatus.Implementation;
using Elevator_Simulator.Elevator.Features.ElevatorStatus.Interface;
using Elevator_Simulator.Model;
using Elevator_Simulator.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

await DoWorkAsync();
void SetupServices(out ServiceProvider _services, out IRequestTheElevator _buildingManagerCaptureUserRequestService, 
                   out IConfigureBuilding _buildingManagerConfigureBuildingService, out IFirstClosestElevator _elevatorManagerFirstClosestElevatorService, 
                   out IAssignElevatorRequest _elevatorManagerAssignElevatorRequestService, out IMoveToDestination _elevatorMovementToDestinationService, 
                   out IElevatorStatus _elevatorStatusService, out IBuildingStatus _buildingBuildingStatusService, out IClearElevator _elevatorEvacuatePassengersService)
{
    _services = CreateServices();
    _elevatorManagerAssignElevatorRequestService = _services.GetRequiredService<IAssignElevatorRequest>();
    _elevatorManagerFirstClosestElevatorService = _services.GetRequiredService<IFirstClosestElevator>();
    _elevatorMovementToDestinationService = _services.GetRequiredService<IMoveToDestination>();
    _elevatorStatusService = _services.GetRequiredService<IElevatorStatus>();
    _buildingManagerCaptureUserRequestService = _services.GetRequiredService<IRequestTheElevator>();
    _buildingManagerConfigureBuildingService = _services.GetRequiredService<IConfigureBuilding>();
    _buildingBuildingStatusService = _services.GetRequiredService<IBuildingStatus>();
    _elevatorEvacuatePassengersService = _services.GetRequiredService<IClearElevator>();
}

ServiceProvider CreateServices()
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
        .AddSingleton<IBuildingStatus, BuildingStatus>()
        .AddSingleton<IClearElevator, ClearElevator>()
        .BuildServiceProvider();

    return serviceProvider;
}

async Task DoWorkAsync()
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
        IBuildingStatus _buildingBuildingStatusService;
        IClearElevator _elevatorEvacuatePassengersService;
        List<Elevator> _tempElevators = new List<Elevator>();

        //Setup the application dependencies
        SetupServices(out _services, out _buildingManagerCaptureUserRequestService, out _buildingManagerConfigureBuildingService,
        out _elevatorManagerFirstClosestElevatorService, out _elevatorManagerAssignElevatorRequestService, out _elevatorMovementToDestinationService,
        out _elevatorStatusService, out _buildingBuildingStatusService, out _elevatorEvacuatePassengersService);

        var _logger = _services.GetRequiredService<ILogger<Program>>();

        //Configure the building
        var building = await _buildingManagerConfigureBuildingService.ConfigureBuildingAsync();

        if (building == null)
        {
            throw new Exception(string.Format("{0} - {1}", DateTime.Now, "Application had problems configuring the building."));
        }
        _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, "Application starting up....."));

        loader();

        Console.WriteLine("\n");

        _logger.LogInformation(string.Format("{0} - {1}", DateTime.Now, "Building is now all setup. Now let's get passengers onto the elevators so they can be safely delivered to their desired destination."));

        await Task.Delay(2000);

        string? userInput = string.Empty;
        do
        {
            Console.WriteLine("----------------------------------------------");

            Console.Clear();
            await _buildingBuildingStatusService.DisplayBuildingStatusAsync(building);

            await _elevatorStatusService.DisplayElevatorStatusAsync(building.Elevators ?? _tempElevators);

            //Request an elevator

            var elevatorRequest = await _buildingManagerCaptureUserRequestService.RequestTheElevatorAsync(building.MaximumPassengers ?? 0, building.TotalFloors ?? 0);

            //Find closet elevator

            if (elevatorRequest == null)
                throw new Exception(string.Format("{0} - {1}", DateTime.Now, "Application had problems receiving user elevator requests."));

            var closestElevator = await _elevatorManagerFirstClosestElevatorService.FindClosestElevatorAvailableAsync(building.Elevators ?? _tempElevators, elevatorRequest.CurrentFloor ?? 0, elevatorRequest.PassengerCount ?? 0);

            if (closestElevator == null)
                Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, $"No elevators available for floor {elevatorRequest.CurrentFloor} to carry {elevatorRequest.PassengerCount ?? 0} passenger(s)."));
            else
            {
                //Assign Request to elevator

                building = await _elevatorManagerAssignElevatorRequestService.AssignRequestAsync(closestElevator, elevatorRequest.CurrentFloor ?? 0, elevatorRequest.PassengerCount ?? 0, elevatorRequest.DestinationFloor ?? 0, building);

                //Move Elevator to destination

                building = await _elevatorMovementToDestinationService.MoveToDestinationAsync(closestElevator, elevatorRequest.DestinationFloor ?? 0, building);
            }

            //Display elevator status

            await _elevatorStatusService.DisplayElevatorStatusAsync(building.Elevators ?? _tempElevators);

            Console.WriteLine("\nType 'exit' to quit or press Enter to continue.\n\nType 'clear' to auto-offload if maximum elevator capacity is reached.");
            userInput = Console.ReadLine();

            if (userInput.Equals("clear"))
            {
                building = await _elevatorEvacuatePassengersService.ClearAllPassengersAsync(building);
                loader();
            }

        } while (userInput != "exit");

        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, $"{ex.Message}"));
    }
}

static void loader()
{
    Helper.WriteProgressBar(0);
    for (var i = 0; i <= 100; ++i)
    {
        Helper.WriteProgressBar(i, true);
        Thread.Sleep(25);
    }
}