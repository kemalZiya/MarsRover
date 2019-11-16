using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Navigator.Objects
{
    public class Movement : IMovement
    {
        public IRoverPosition Rover { get; set; }
        public IInstructions Instructions { get; set; }
    }
}
