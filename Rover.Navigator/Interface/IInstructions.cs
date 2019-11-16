using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Navigator
{
    public interface IInstructions
    {
       InstructionCommands[] Commands { get; set; }
    }
}
