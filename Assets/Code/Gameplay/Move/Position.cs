using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct Position
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
    }
}
