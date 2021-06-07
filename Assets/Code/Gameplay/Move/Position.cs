using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct Position : ISerializable
    {
        public const int 
            UNDEFINED = -4,
            FIRST = -3,
            LAST = -2,
            RANDOM = -1;

        public static readonly Position Null = new Position { Index = UNDEFINED, Location = ELocation.Undefined, Id = 0 };

        public int Id;
        public ELocation Location;
        public int Index;

        public bool IsExist => Location != ELocation.Undefined;


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

        public Location GetLocation(BoardManager manager)
        {
            if (Location == ELocation.Field)
            {
                return manager.Battlefield.GetTile(Id);
            }
            else
            {
                BoardSide side = manager.GetBoardSide((EPlayer)Id);
                Location loc = side.GetLocation(Location);
                return loc;
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Id)
                .Write(Location)
                .Write(Index);
        }

        public static bool operator ==(Position a, Position b)
        {
            return a.Id == b.Id &&
                a.Index == b.Index &&
                a.Location == b.Location;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Postion]");
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"Location = {Location}");
            sb.AppendLine($"Index = {Index}");

            return sb.ToString();
        }
    }
}
