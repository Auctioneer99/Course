using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Tile : Location
    {
        public TileDefinition Definition { get; private set; }

        public Tile(BoardSide side, TileDefinition definition) : base(side, ELocation.Field, 1) 
        {
            Definition = definition;
        }


    }
}
