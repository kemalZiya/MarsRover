using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Rover.Navigator
{
    public enum Directions
    {
        [Description("N")]
        North = 1,

        [Description("E")]
        East = 2,

        [Description("S")]
        South = 3,

        [Description("W")]
        West = 4
    }
}
