using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Navigator
{
    public class Instructions : IInstructions
    {
       public InstructionCommands[] Commands { get; set; }
    }
}
