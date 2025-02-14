// See https://aka.ms/new-console-template for more information
using RobotSim;
var robot = new RobotHelper();

while (true)
{
    Console.Write("Please Enter your Command (or type 'exit' to quit):");
    string input = Console.ReadLine();

    if (input.ToLower() == "exit")
    {
        Console.WriteLine("Exiting Robot Simulator...");
        break;
    }

    robot.ExecuteCommand(input);
}
