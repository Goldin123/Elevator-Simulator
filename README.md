# Building Elevators Manager

This is a C# .net 8 console application propotype designed to simulate elevators movement within a specific building. The application first configures all building details needed to get things started and then all you need to do is use a simple input panel to perfom a series of elevator simulation. The application also determins the closest available elevator on request then after the passengers have boarded, then can then be safely deleivered to their dired destinations. The application follows strict SOLID princeples standards and user activity is neatly displays on the main console window.  

## Description

Once the application is running, the user will first be prompted to enter the number of floors the building has, the nubmer of elevators the building has and also the maximum capacity each elevator can carry. Once this is setup, a unique set of elevators will be generated by the system and each diplayed with uniuque features. The system will then prompt the user the current floor their on, the number of passenger and then the furthest floor the passengers want to travel to. On each floor, the system will prompt the uses as to how many passengers want to get off anf keeps a track of how many passengers are still in the elevator until the desired destination is reached. The application does a series of input validation on each user entry anf responds with appropriate guidlines where required. 

## Getting Started

### Dependencies

* Please make sure you have .net 8 sdk installed.
* Please make sure you have the latest Visual Studio installed , version 17.8.3 or Higher.  

### Executing program

* Plase first build the solution and make sure all project dependencies are installed.
* Once build is successful, simply run the project. Make sure 'Elevator-Simulator' in the Application folder is set as a start up project.

## How the program works

Once the application builds successfully, there are a couple of steps needed first before the simulation starts.

### Step 1: Configure the Building

![Configure Building Process](https://github.com/Goldin123/Elevator-Simulator/assets/17449653/fbd6e95d-7161-4a6c-8320-8003fea96b5d)


To achieve this, the application will prompt a user to enter the following, the total number of floors available, the total number of elevators available, and the maximum capacity each elevator can carry. All the inputs are validated to make sure that the information supplied is in the correct form, i.e., No negative, Only numbers, etc.

### Step 2: Manage Elevators

Once the building is configured, the application will always need the following inputs to perform a simulation, the current floor, the number of passengers, and the furthest desired destination floor to travel. Like the building setup, these inputs are also validated. Note if at any instance that the elevators are all full, the user will be notified. To offload all passengers in all the elevators, a special command called 'clear' can be used to offload all the passengers in all the elevators.

## Technology Stack

| Technology | Version |
| --- | --- |
| .net 8 | verion 8.0.100 |
| Visual Studio | version 17.8.3 |
| XUnit | vaersion 2.6.5 |

## Authors

Contributors names and contact info

ex. Goldin Nyiko Baloyi  


## License

This project is licensed under the [MIT] License - see the LICENSE.md file for details
