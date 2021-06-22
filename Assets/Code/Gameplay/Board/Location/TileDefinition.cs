using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public struct TileDefinition
    {
        public int Id => GetHashCode();

        readonly public short X, Y;

        readonly public EPlayer Player;

        public short Z => (short)(-X - Y);

        public TileDefinition(EPlayer player, short x, short y)
        {
            X = x;
            Y = y;
            Player = player;
        }

        public int AbsoluteDistance(TileDefinition destination)
        {
            return Math.Abs((X - destination.X) + (Y - destination.Y));
        }

        public override int GetHashCode()
        {
            return 31 * X + Y;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TileDefinition");
            sb.AppendLine($"X = {X}");
            sb.AppendLine($"Y = {Y}");
            sb.AppendLine($"Player = {Player}");
            sb.AppendLine($"Id = {Id}");
            return sb.ToString();
        }
    }
}
