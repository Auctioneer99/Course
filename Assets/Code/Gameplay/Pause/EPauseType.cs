using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public enum EPauseType
    {
        Logic = 0,
        Timers = 1 << 0,
        Animation = 1 << 1,
        Movement = 1 << 2,

        All = Logic | Timers | Animation | Movement,
    }

    public static class EPauseTypeExtension
    {
        public static readonly EPauseType[] Array = (EPauseType[])Enum.GetValues(typeof(EPauseType));

        public static bool Contains(this EPauseType type, EPauseType other)
        {
            return (type & other) != 0;
        }
    }
}
