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

        public short Z => (short)(-X - Y);

        public TileDefinition(short x, short y)
        {
            X = x;
            Y = y;
        }

        public override int GetHashCode()
        {
            return 31 * X + Y;
        }
    }
}
