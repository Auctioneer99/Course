using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public enum EPlayerStatus
    {
        Loading = 0,
        Ready = 1 << 0,
        Active = 1 << 1,
        Finished = 1 << 2,
        Blocked = 1 << 3,

        Communicable = Loading | Ready | Active | Finished
    }

    public static class EPlayerStatusExtension
    {
        public static bool Contains(this EPlayerStatus status, EPlayerStatus other)
        {
            return (status & other) > 0;
        }
    }
}
