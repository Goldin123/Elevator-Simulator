using System;
using System.Collections.Generic;
using System.Threading.Tasks;

interface IElevatorMovement
{
    Task MoveToAsync(int destinationFloor);
}

class Elevator : IElevatorMovement
{
    public int Number { get; }
    public int CurrentFloor { get; set; }
    public int PassengerCount { get; set; }
    public int DestinationFloor { get; set; }
    public string Direction { get; set; }
    public int CurrentTravelFloor { get; set; }

    public Elevator(int number, int currentFloor)
    {
        Number = number;
        CurrentFloor = currentFloor;
        PassengerCount = 0;
        DestinationFloor = currentFloor;
        Direction = "Idle";
        CurrentTravelFloor = currentFloor;
    }

    public async Task MoveToAsync(int destinationFloor)
    {
        Direction = destinationFloor > CurrentFloor ? "Up" : "Down";

        while (CurrentTravelFloor != destinationFloor)
        {
            Console.WriteLine($"Elevator {Number} moving from floor {CurrentTravelFloor} to floor {CurrentTravelFloor + (Direction == "Up" ? 1 : -1)} ({Direction})");
            await Task.Delay(1000); // Simulate delay for real-time movement
            CurrentTravelFloor += Direction == "Up" ? 1 : -1;
        }

        CurrentFloor = destinationFloor;
        CurrentTravelFloor = destinationFloor;
        Direction = "Idle";
    }
}

class ElevatorManager
{
    private readonly List<Elevator> elevators;

    public ElevatorManager(List<Elevator> elevators)
    {
        this.elevators = elevators;
    }

    public Elevator FindClosestElevator(int currentFloor)
    {
        int minDistance = int.MaxValue;
        Elevator closestElevator = null;

        foreach (var elevator in elevators)
        {
            int distance = Math.Abs(elevator.CurrentFloor - currentFloor);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestElevator = elevator;
            }
        }

        return closestElevator;
    }

    public void AssignRequest(Elevator elevator, int currentFloor, int passengerCount, int destinationFloor)
    {
        if (elevator.Direction == "Idle")
        {
            elevator.CurrentFloor = currentFloor;
            elevator.PassengerCount = passengerCount;
            elevator.DestinationFloor = destinationFloor;
            elevator.CurrentTravelFloor = currentFloor; // Reset the current travel floor
        }
        else
        {
            Console.WriteLine($"Elevator {elevator.Number} is currently in motion. Request assigned to it will be processed after it reaches its destination.");
        }
    }
}

class Building
{
    static async Task Main()
    {
        Console.WriteLine("Enter the number of floors in the building:");
        int totalFloors = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the number of elevators in the building:");
        int elevatorCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the maximum capacity of each elevator:");
        int maxCapacity = int.Parse(Console.ReadLine());

        List<Elevator> elevators = new List<Elevator>();
        for (int i = 1; i <= elevatorCount; i++)
        {
            elevators.Add(new Elevator(i, 1)); // All elevators start at the first floor
        }

        ElevatorManager elevatorManager = new ElevatorManager(elevators);

        string userInput;
        do
        {
            Console.WriteLine("\nEnter details for a new request (type 'exit' to quit):");

            Console.Write("Current floor: ");
            int currentFloor;
            if (!int.TryParse(Console.ReadLine(), out currentFloor))
                break;

            Console.Write("Number of passengers: ");
            int passengerCount;
            if (!int.TryParse(Console.ReadLine(), out passengerCount))
                break;

            Console.Write("Destination floor: ");
            int destinationFloor;
            if (!int.TryParse(Console.ReadLine(), out destinationFloor))
                break;

            Elevator closestElevator = elevatorManager.FindClosestElevator(currentFloor);

            elevatorManager.AssignRequest(closestElevator, currentFloor, passengerCount, destinationFloor);

            await closestElevator.MoveToAsync(destinationFloor);

            DisplayElevatorStatus(elevators);

            Console.WriteLine("\nType 'exit' to quit or press Enter to continue.");
            userInput = Console.ReadLine();

        } while (userInput != "exit");
    }

    static void DisplayElevatorStatus(List<Elevator> elevators)
    {
        Console.WriteLine("\nElevator Status:");

        foreach (var elevator in elevators)
        {
            Console.WriteLine($"Elevator {elevator.Number} at floor {elevator.CurrentFloor}, " +
                              $"Passenger count: {elevator.PassengerCount}, " +
                              $"Destination floor: {elevator.DestinationFloor}, " +
                              $"Direction: {elevator.Direction}");
        }
    }
}
