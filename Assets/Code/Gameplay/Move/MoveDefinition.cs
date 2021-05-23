using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct MoveDefinition
    {
        public int CardId;
        public Position From, To;
        public EPlayer MovedByPlayer;

        public MoveDefinition(Card card, Position to, EPlayer movedByPlayer)
        {
            CardId = card.Id;
            From = card.Position;
            To = to;
            MovedByPlayer = movedByPlayer;
        }
    }
}
