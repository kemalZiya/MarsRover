using Rover.Navigator.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover.Navigator.Services
{
    public class RoverNavigator : IRoverNavigator
    {
        public ICoordinates UpperRightCoordinates { get; private set; }
        public ICoordinates LowerLeftCoordinates { get; private set; }
        private List<IRoverPosition> Rovers { get; set; }
        public void SetCoordinates(ICoordinates lowerLeftCoordinates, ICoordinates upperRightCoordinates)
        {
            UpperRightCoordinates = upperRightCoordinates;
            LowerLeftCoordinates = lowerLeftCoordinates;
        }
      
        public IRoverPosition[] Execute(IMovement[] movements)
        {
            Rovers = new List<IRoverPosition>();

            ValidateInitalData();

            foreach (var movement in movements)
            {
                ValidateMovement(movement);
                ValidateRover(movement.Rover, movement.Rover);
                ExecuteInstructions(movement);
                Rovers.Add(movement.Rover);
            }

            return Rovers.ToArray();
        }
        private void ExecuteInstructions(IMovement movement)
        {
            
            foreach (var command in movement.Instructions.Commands)
            {
                if(command == InstructionCommands.Move)
                {
                    movement.Rover = Move(movement.Rover);
                    continue;
                }

                SetDirection(movement.Rover, command);
            }
        }
        private IRoverPosition SetDirection(IRoverPosition rover, InstructionCommands command)
        {
            var direction = rover.Direction;
            var maxValue = Enum.GetValues(typeof(Directions)).Length;
            var minValue = 1;

            if (command == InstructionCommands.Right)
            {
                if ((int)direction == maxValue)
                    direction = (direction - (maxValue - 1));
                else
                    direction += 1;
            }

            if (command == InstructionCommands.Left)
            {
                if ((int)direction == minValue)
                    direction = (direction + (maxValue - 1));
                else
                    direction -= 1;
            }

            rover.Direction = direction;

            return rover;
        }
        private IRoverPosition Move(IRoverPosition starPosition)
        {
            IRoverPosition endPosition = new RoverPosition() { Direction = starPosition.Direction, X = starPosition.X, Y = starPosition.Y };

            if(endPosition.Direction == Directions.North)
            {
                endPosition.Y += 1;
            }

            if(endPosition.Direction == Directions.South)
            {
                endPosition.Y -= 1;
            }

            if (endPosition.Direction == Directions.East)
            {
                endPosition.X += 1;
            }

            if (endPosition.Direction == Directions.West)
            {
                endPosition.X -= 1;
            }
       
            ValidateRover(starPosition, endPosition);

            starPosition = endPosition;

            return starPosition;
        }
        private void ValidateInitalData()
        {
            var subject = "ValidateInitalData";
            if (LowerLeftCoordinates == null)
            {
                throw new Exception($"{subject}: Lower-Left coordinate is undefined");
            }

            if (UpperRightCoordinates == null)
            {
                throw new Exception($"{subject}: Upper-Right coordinate is undefined");
            }

        }
        private void ValidateMovement(IMovement movement)
        {
            var subject = "ValidateMovement";
            if (movement.Rover == null)
                throw new Exception($"{subject}: Rover position is null");

            if (movement.Instructions == null)
                throw new Exception($"{subject}: Instructions is null");
        }
        private void ValidateRover(IRoverPosition startPosition, IRoverPosition endPosition)
        {
            var subject = "ValidateRover";
            var isValid = true;

            if (UpperRightCoordinates.X < endPosition.X)
                isValid = false;

            if (LowerLeftCoordinates.X > endPosition.X)
                isValid = false;

            if (UpperRightCoordinates.Y < endPosition.Y)
                isValid = false;

            if (LowerLeftCoordinates.Y > endPosition.Y)
                isValid = false;


            var roverPosition = $"RoverPosition (Start: [{startPosition.X} {startPosition.Y} {startPosition.Direction}]), End [{endPosition.X} {endPosition.Y} {endPosition.Direction}]";

            if (!isValid)
                throw new Exception($"{subject}: Rover is out of boundaries. {roverPosition}");

            if (Rovers.Any(f => f.X == endPosition.X && f.Y == endPosition.Y))
                throw new Exception($"{subject}: The is another rover at the same coordinate. {roverPosition}");
        }
     
    }
}
