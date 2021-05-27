using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public enum ECardVisibility : byte
    {
        Noone = 0,
        Owner = 1 << 0,
        Enemy = 1 << 1,
        All = Owner | Enemy
    }

    public static class ECardVisibilityExtension
    {
        public static Packet Write(this Packet packet, ECardVisibility visibility)
        {
            packet.Write((byte)visibility);
            return packet;
        }

        public static ECardVisibility ReadECardVisibility(this Packet packet)
        {
            return (ECardVisibility)packet.ReadByte();
        }

        public static bool IsVisibleTo(this ECardVisibility visibility, EPlayer owner, EPlayer target)
        {
            if (target.Contains(owner))
            {
                return visibility.Contains(ECardVisibility.Owner);
            }
            else
            {
                return visibility.Contains(ECardVisibility.Enemy);
            }

        }

        public static bool Contains(this ECardVisibility visibility, ECardVisibility other)
        {
            return (visibility & other) != 0;
        }
    }
}
