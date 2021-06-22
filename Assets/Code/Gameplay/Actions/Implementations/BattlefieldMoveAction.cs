using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    class BattlefieldMoveAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.BattlefieldMove;

        public BattlefieldMoveDefinition Definition { get; private set; }

        public BattlefieldMoveAction Initialize(ushort cardId, EPlayer movedBy)
        {
            base.Initialize();

            Definition = new BattlefieldMoveDefinition(cardId, movedBy);
            return this;
        }

        protected override void ApplyImplementation()
        {
            Card card = GameController.CardManager.GetCard(Definition.CardId);
            GameController.BoardManager.BattlefieldMove(card, Definition.Path);
        }

        protected override void AttributesFrom(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void AttributesTo(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            BattlefieldMoveAction other = copyFrom as BattlefieldMoveAction;

            Definition = other.Definition;
        }
    }
}
