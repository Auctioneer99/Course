using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class MoveAction : AAction, IAuthorityAction, IVisibilityChangeRequester
    {
        public override EAction EAction => EAction.Move;

        public List<MoveDefinition> Moves { get; private set; }

        public new MoveAction Initialize()
        {
            base.Initialize();

            Moves = new List<MoveDefinition>();
            return this;
        }

        protected override void ApplyImplementation()
        {
            GameController.VisibilityManager.ChangeVisibility(this);

            foreach (var def in Moves)
            {
                Card card = GameController.CardManager.GetCard(def.CardId);
                GameController.BoardManager.Move(card, def.To);
            }
        }

        public void AddCardsToChangeVisibility(ChangeVisibilityAction action)
        {
            foreach(var move in Moves)
            {
                Card card = GameController.CardManager.GetCard(move.CardId);
                bool shouldHide = move.From.Player == move.To.Player && ELocation.Field.Contains(move.To.Location);

                action.Add(card, VisibilityManager.GetTargetVisibilityWhenMoving(card, move.From, move.To), move.To.Player, shouldHide);
            }
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
            MoveAction other = copyFrom as MoveAction;

            Moves = new List<MoveDefinition>(other.Moves);
        }
    }
}
