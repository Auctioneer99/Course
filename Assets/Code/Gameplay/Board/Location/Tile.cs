using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Tile : Location
    {
        public int Id => Definition.Id;

        public TileDefinition Definition { get; private set; }

        public Tile(BoardSide side, TileDefinition definition) : base(side, ELocation.Field, 1) 
        {
            Definition = definition;
        }

        protected override void UpdateIndexes()
        {
            Position position = new Position(Definition);
            for (int i = 0, count = Cards.Count; i < count; i++)
            {
                position.Index = i;
                Cards[i].Position = position;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.AppendLine($"Definition = {Definition.ToString()}");
            return sb.ToString();
        }
    }
}
