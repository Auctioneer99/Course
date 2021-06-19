using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class ChangeVisibilityAction : AAction
    {
        public override EAction EAction => EAction.ChangeVisibility;

        public List<VisibilityChangeDefinition> Changes { get; private set; }

        public new ChangeVisibilityAction Initialize()
        {
            base.Initialize();

            Changes = new List<VisibilityChangeDefinition>(4);

            return this;
        }

        public void Add(Card card, ECardVisibility targetVisibility, EPlayer targetPlayer = EPlayer.Undefined, bool shouldBeHiddenForClient = true)
        {
            if (targetPlayer == EPlayer.Undefined)
            {
                targetPlayer = card.Position.Player;
            }

            var definition = new VisibilityChangeDefinition(card.Id, targetVisibility, targetPlayer, shouldBeHiddenForClient);
            Changes.Add(definition);
        }

        protected override void ApplyImplementation()
        {
            foreach(var def in Changes)
            {
                Card card = GameController.CardManager.GetCard(def.Card);
                card.EVisibility = def.TargetVisibility;
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
            ChangeVisibilityAction other = copyFrom as ChangeVisibilityAction;

            Changes = new List<VisibilityChangeDefinition>(other.Changes);
        }
    }
}
