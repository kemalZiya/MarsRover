using System.Collections.Generic;

namespace Rover.Navigator
{
    public interface IRoverNavigator
    {
        ICoordinates UpperRightCoordinates { get; }
        ICoordinates LowerLeftCoordinates { get; }
        void SetCoordinates(ICoordinates lowerLeftCoordinates, ICoordinates upperRightCoordinates);
        IRoverPosition[] Execute(IMovement[] movements);
    }
}
