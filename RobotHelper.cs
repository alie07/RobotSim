using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
    internal class RobotHelper
    {

        public RobotHelper()
        {
            Console.WriteLine("Welcome to Robot Simulator");
        }

        enum Direction
        {
            North,
            East,
            South,
            West
        }

        enum Command
        {
            Place,
            Move,
            Left,
            Right,
            Report
        }
        
        int x = 0;
        int y = 0;
        Direction direction = Direction.North;
        bool isPlaced = false;
        Command command;

        public void ExecuteCommand(string input)
        {
            string[] commandParts = input.Split(' ');
            if (!IsValidCommand(commandParts[0]))
            {
                Console.WriteLine("Unrecognized Command. Please enter a valid command (Place, Move, Left, Right, Report)");
                return;
            }
            if (commandParts.Length < 2&& commandParts[0].ToLower() == "place")
            {
                Console.WriteLine("Invalid Place Command. Example Place command 'Place 0,0,North;");
                return;
            }
            switch (command)
            {
                case Command.Place:
                    Place(commandParts[1]);
                    break;
                case Command.Move:
                    Move();
                    break;
                case Command.Left:
                    Left();
                    break;
                case Command.Right:
                    Right();
                    break;
                case Command.Report:
                    Report();
                    break;
            }
        }

        void Place(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length == 3)
            {
                if (!int.TryParse(parts[0], out x) || x > 4)
                {
                    Console.WriteLine("Invalid X coordinate, Please read Instruction.txt");
                }
                if (!int.TryParse(parts[1], out y) || x > 4)
                {
                    Console.WriteLine("Invalid Y coordinate, Please read Instruction.txt");
                }
                if (!Enum.TryParse(parts[2], true, out direction))
                {
                    Console.WriteLine("Invalid Direction");
                }
                isPlaced = true;
            }
            else
            {
                Console.WriteLine("Invalid Place Command. Please provide X,Y and Direction in the format 'Place X,Y,Direction'");
            }
        }
        void Report()
        {
            Console.WriteLine(string.Format("{0},{1},{2}", x, y, direction));
        }

        void Left()
        {
            if (!isPlaced)
            {
                Console.WriteLine("Please place the robot first");
                return;
            }
            direction = (Direction)(((int)direction + 3) % 4);
        }
        
        void Right()
        {
            if (!isPlaced)
            {
                Console.WriteLine("Please place the robot first");
                return;
            }
            direction = (Direction)(((int)direction + 1) % 4);
        }

        void Move()
        {
            if (!isPlaced)
            {
                Console.WriteLine("Please place the robot first");
                return;
            }
            switch (direction)
            {
                case Direction.North:
                    y++;
                    if (!IsMoveOkay(x, y))
                    {
                        y--;
                        goto _outofrange;
                    }
                    break;
                case Direction.East:
                    x++;
                    if (!IsMoveOkay(x, y))
                    {
                        x--;
                        goto _outofrange;
                    }
                    break;
                case Direction.South:
                    y--;
                    if (!IsMoveOkay(x, y))
                    {
                        y++;
                        goto _outofrange;
                    }
                    break;
                case Direction.West:
                    x--;
                    if (!IsMoveOkay(x, y))
                    {
                        x++;
                        goto _outofrange;
                    }
                    break;
            }
            return;

            _outofrange:
            Console.WriteLine("Command cannot be executed: You've reached the end of the table");
            Console.WriteLine("Current position is: " + x + "," + y + "," + direction);
        }
        

        bool IsMoveOkay(int x, int y)
        {
            return x > -1 && x < 5 && y > -1 && y < 5;
        }

        bool IsValidCommand(string input)
        {
            return Enum.TryParse(input, true, out command);
        }
    }
}
