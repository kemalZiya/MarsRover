using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Rover.Navigator
{
    public enum InstructionCommands
    {
        [Description("L")]
        Left = 1,

        [Description("R")]
        Right = 2,

        [Description("M")]
        Move = 3,

    }
 
}
