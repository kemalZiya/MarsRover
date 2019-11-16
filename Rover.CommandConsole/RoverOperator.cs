using Rover.Common;
using Rover.Navigator;
using Rover.Navigator.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Rover.CommandConsole
{
    public class RoverOperator
    {
        IRoverNavigator _navigator;
        public RoverOperator(IRoverNavigator navigator)
        {
            _navigator = navigator;
        }

        public void Operate()
        {
            try
            {
                Info();
                GetCoordinates();
                var movements = GetRoverMovements();
                Execute(movements);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
        void Info()
        {
            Console.WriteLine("1. Line:  Write upper-right coordinates seperated by a space. Ex: (X Y)");
            Console.WriteLine("2. Line:  Write the rover position and rotation(N: North, E: East, W: West, S: South) seperated by spaces. Ex: (X Y N)");
            Console.WriteLine("3. Line:  Write the rotation (L: Left, R: Right) and movement (M: Move) instructions. Ex:(LMMRMLMR)");
            Console.WriteLine("For the other rovers, you must continue with second and third lines.");
            Console.WriteLine("For the output, you must press [Enter] Key");
            Console.WriteLine("Begin Navigate...!");
        }
        private void GetCoordinates()
        {
            try
            {
                var consoleValue = Console.ReadLine();
                if (consoleValue.Trim().Length > 0)
                {
                    var consolValueArray = consoleValue.Split(" ").Select(s => int.Parse(s)).ToArray();

                    if (consolValueArray.Length != 2)
                    {
                        throw new Exception("GetCoordinates: Input count are not given X and Y coordianate.");
                    }

                    var lowerLeftCoordinates = new Coordinates() { X = 0, Y = 0 };

                    var upperRightCoordinates = new Coordinates()
                    {
                        X = consolValueArray[0],
                        Y = consolValueArray[1]
                    };

                    _navigator.SetCoordinates(lowerLeftCoordinates, upperRightCoordinates);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"GetCoordinates: Cannot get coordinates.Ex: { ex.ToString() }");
            }
        }
        private IMovement[] GetRoverMovements()
        {
            var consoleString = string.Empty;
            List<IMovement> movements = new List<IMovement>();
            IMovement currentMovement = null;
            do
            {
                try
                {
                    consoleString = Console.ReadLine();
                    if (!string.IsNullOrEmpty(consoleString))
                    {
                        if (currentMovement == null)
                        {
                            currentMovement = new Movement();
                            currentMovement.Rover = GetRoverPosition(consoleString);

                            if (movements.Any(f => f.Rover.X == currentMovement.Rover.X && f.Rover.Y == currentMovement.Rover.Y))
                                throw new Exception($"More than one rover at same coordinates. Coordinates: [X:{currentMovement.Rover.X}, Y:{currentMovement.Rover.Y}]");
                        }
                        else
                        {
                            currentMovement.Instructions = GetInstructions(consoleString);
                            movements.Add(currentMovement);
                            currentMovement = null;
                        }
                    }
                    else break;
                }
                catch (Exception ex)
                {
                    throw new Exception($"GetRoverMovements: Cannot get rover movements. Ex: {ex.ToString()}");
                }
               
            }
            while (!string.IsNullOrEmpty(consoleString.Trim()));

            if (movements.Count == 0)
                throw new Exception($"GetRoverMovements: Cannot set movements properly.");

            return movements.ToArray();
        }
        private IRoverPosition GetRoverPosition(string roverString)
        {
            IRoverPosition roverPosition = null;
            var list = roverString.Trim().Split(" ");
            if (list.Length == 3)
            {
                roverPosition = new RoverPosition();

                roverPosition.X = int.Parse(list[0]);
                roverPosition.Y = int.Parse(list[1]);
                roverPosition.Direction = EnumHelper.GetEnumValue<Directions>(list[2]);

                if (roverPosition.Direction == 0)
                    throw new Exception("(GetRoverPosition) Cannot get a direction command");
            }
            else
                throw new Exception("(GetRoverPosition) Cannot get inputs properly");

            return roverPosition;
        }
        private IInstructions GetInstructions(string instructionString)
        {
            IInstructions instructions = null;
            var commands = instructionString.Trim().Select(s => EnumHelper.GetEnumValue<InstructionCommands>(s.ToString().ToUpper())).ToArray();

            if(commands.Length == 0)
                throw new Exception("(GetInstructions) Cannot get any instruction command");
            if(commands.Any(a => a == 0))
                throw new Exception("(GetInstructions) Cannot get any instructions properly");

            instructions = new Instructions();
            instructions.Commands = commands;

            return instructions;
        }
        private void Execute(IMovement[] movements)
        {
            var rovers = _navigator.Execute(movements);

            foreach (var rover in rovers)
            {
                Console.WriteLine($"{rover.X} {rover.Y} {rover.Direction.GetDescription()}");
            }

            Console.ReadKey();
        }
        
    }

}
