namespace Rover.Navigator.Objects
{
    public class RoverPosition : IRoverPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Direction { get; set; }
    }
}
