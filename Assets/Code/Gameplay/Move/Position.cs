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


        public EPlayer Player;
        public ELocation Location;
        public int Index;

        public Position(EPlayer player, ELocation location, int index = LAST)
        {
            Player = player;
            Location = location;
            Index = index;
        }

        public Position(Packet packet)
        {
            Player = packet.ReadEPlayer();
            Location = packet.ReadELocation();
            Index = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Player)
                .Write(Location)
                .Write(Index);
        }
    }
}
