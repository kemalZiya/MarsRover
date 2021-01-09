using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Navigator;
using Rover.Navigator.Objects;
using Rover.Navigator.Services;

namespace Rover.Tests
{
    [TestClass]
    public class RoverNavigatorTest
    {
        private RoverNavigator _roverNavigator;
        private Coordinates _lowerLeftCoordinates;
        private Coordinates _upperRightCoordinates;
        /*
         Test Input:
         5 5
         1 2 N
         LMLMLMLMM
         3 3 E
         MMRMMRMRRM
         Expected Output:
         1 3 N
         5 1 E
             */
        [TestInitialize]
        public void Initialize()
        {
            _roverNavigator = new RoverNavigator();
            _lowerLeftCoordinates = new Coordinates()
             {
                 X = 0,
                 Y = 0
             };
            _upperRightCoordinates = new Coordinates()
            {
                X = 5,
                Y = 5
            };
        }
        [TestMethod]
        public void SetCoordinates()
        {
            _roverNavigator.SetCoordinates(_lowerLeftCoordinates, _upperRightCoordinates);
            Assert.AreEqual(_roverNavigator.LowerLeftCoordinates, _lowerLeftCoordinates);
            Assert.AreEqual(_roverNavigator.UpperRightCoordinates, _upperRightCoordinates);
        }

        [TestMethod]
        public void Execute()
        {
            _roverNavigator.SetCoordinates(_lowerLeftCoordinates, _upperRightCoordinates);

            Movement[] movements = new Movement[2];
            var movement = new Movement();
            //1 2 N
            movement.Rover = new RoverPosition()
            {
                Direction = Navigator.Directions.North,
                X = 1,
                Y = 2
            };

            //LMLMLMLMM
            movement.Instructions = new Instructions()
            {
                Commands = new InstructionCommands[]
                 {
                     InstructionCommands.Left,
                     InstructionCommands.Move,
                     InstructionCommands.Left,
                     InstructionCommands.Move,
                     InstructionCommands.Left,
                     InstructionCommands.Move,
                     InstructionCommands.Left,
                     InstructionCommands.Move,
                     InstructionCommands.Move
                 }
            };
            movements[0] = movement;
           
           
            var movementNew = new Movement();
            //3 3 E
            movementNew.Rover = new RoverPosition()
            {
                Direction = Navigator.Directions.East,
                X = 3,
                Y = 3
            };

            //MMRMMRMRRM
            movementNew.Instructions = new Instructions()
            {
                Commands = new InstructionCommands[]
                 {
                     InstructionCommands.Move,
                     InstructionCommands.Move,
                     InstructionCommands.Right,
                     InstructionCommands.Move,
                     InstructionCommands.Move,
                     InstructionCommands.Right,
                     InstructionCommands.Move,
                     InstructionCommands.Right,
                     InstructionCommands.Right,
                     InstructionCommands.Move
                 }
            };
            movements[1] = movementNew;

            IRoverPosition[] roverPositions = _roverNavigator.Execute(movements);

            /*
             Expected Output:
                1 3 N
                5 1 E
             */

            var roverPositionsCaseOne = new RoverPosition() { Direction = Directions.North, X = 1, Y = 3 };
            var roverPositionsCaseTwo = new RoverPosition() { Direction = Directions.East, X = 5, Y = 1 };
           
            Assert.AreEqual(roverPositionsCaseOne.X, roverPositions[0].X);
            Assert.AreEqual(roverPositionsCaseOne.Y, roverPositions[0].Y);
            Assert.AreEqual(roverPositionsCaseOne.Direction, roverPositions[0].Direction);
            Assert.AreEqual(roverPositionsCaseTwo.X, roverPositions[1].X);
            Assert.AreEqual(roverPositionsCaseTwo.Y, roverPositions[1].Y);
            Assert.AreEqual(roverPositionsCaseTwo.Direction, roverPositions[1].Direction);

        }

        [TestCleanup]
        public void Cleanup()
        {
            _roverNavigator = null;
        }
    }
}
