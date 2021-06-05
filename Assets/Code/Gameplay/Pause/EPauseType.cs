using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public enum EPauseType : byte
    {
        Undefined = 0,
        Logic = 1 << 0,
        Timers = 1 << 1,
        Animation = 1 << 2,
        Movement = 1 << 3,
    }

    public static class EPauseTypeExtension
    {
        public static readonly EPauseType[] Array = (EPauseType[])Enum.GetValues(typeof(EPauseType));

        public static Packet Write(this Packet packet, EPauseType type)
        {
            return packet.Write((byte)type);
        }

        public static EPauseType ReadEPauseType(this Packet packet)
        {
            return (EPauseType)packet.ReadByte();
        }

        public static bool Contains(this EPauseType type, EPauseType other)
        {
            return (type & other) != 0;
        }
    }
}
