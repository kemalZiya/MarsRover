namespace Rover.Navigator
{
    public interface IRoverPosition : ICoordinates
    {
        Directions Direction { get; set; }
    }
}
