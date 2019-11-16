namespace Rover.Navigator
{
    public interface IMovement
    {
        IRoverPosition Rover { get; set; }
        IInstructions Instructions { get; set; }
    }
}
