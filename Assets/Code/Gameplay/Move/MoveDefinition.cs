using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct MoveDefinition
    {
        public ushort CardId;
        public Position From, To;
        public EPlayer MovedByPlayer;

        public MoveDefinition(Card card, Position to, EPlayer movedByPlayer)
        {
            CardId = card.Id;
            From = card.Position;
            To = to;
            MovedByPlayer = movedByPlayer;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[MoveDefinition]");
            sb.AppendLine($"CardId = {CardId}");
            sb.Append($"From = {From.ToString()}");
            sb.Append($"To = {To.ToString()}");
            sb.AppendLine($"MovedBy = {MovedByPlayer}");
            return sb.ToString();
        }
    }
}
