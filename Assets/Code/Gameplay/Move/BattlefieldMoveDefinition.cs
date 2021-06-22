using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct BattlefieldMoveDefinition : IStateObject<BattlefieldMoveDefinition>
    {
        public ushort CardId;
        public EPlayer MovedBy;
        public List<TileDefinition> Path;

        public BattlefieldMoveDefinition(ushort cardId, EPlayer movedBy)
        {
            CardId = cardId;
            MovedBy = movedBy;
            Path = new List<TileDefinition>();
        }

        public void Add(TileDefinition tile)
        {
            if (Path.Count > 0)
            {
                if (Path.Last().AbsoluteDistance(tile) != 1)
                {
                    throw new Exception("Card on battlefield can be moved by only one tile away from previous");
                }
            }
            Path.Add(tile);
        }

        public void Copy(BattlefieldMoveDefinition other)
        {
            CardId = other.CardId;
            MovedBy = other.MovedBy;
            UnityEngine.Debug.Log(other.Path.Count);
            Path = new List<TileDefinition>(other.Path);
            UnityEngine.Debug.Log(Path.Count);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[BattlefieldMoveDefinition]");
            sb.AppendLine($"CardId = {CardId}");
            sb.AppendLine($"MovedBy = {MovedBy}");
            sb.AppendLine("Path:");
            foreach(var point in Path)
            {
                sb.Append(point.ToString());
            }
            return sb.ToString();
        }
    }
}
