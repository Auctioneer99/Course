using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct Position : ISerializable
    {
        public const int UNDEFINED = -4,
        FIRST = -3,
        LAST = -2,
        RANDOM = -1;

        public static readonly Position Null = new Position { Index = UNDEFINED, Location = ELocation.Undefined, Id = 0 };

        public int Id;
        //public EPlayer Player;
        public ELocation Location;
        public int Index;

        public Position(int id, ELocation location, int index = LAST)
        {
            Id = id;
            Location = location;
            Index = index;
        }

        public Position(Packet packet)
        {
            Id = packet.ReadInt();
            Location = packet.ReadELocation();
            Index = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Id)
                .Write(Location)
                .Write(Index);
        }
    }
}
