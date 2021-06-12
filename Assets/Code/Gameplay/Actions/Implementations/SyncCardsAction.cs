using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class SyncCardsAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SyncCards;

        public EPlayer Player { get; private set; }

        public List<Card> CardsToReveal { get; private set; }
        public List<ushort> CardsToHide { get; private set; }

        public SyncCardsAction Initialize(EPlayer player, ChangeVisibilityAction action)
        {
            base.Initialize();

            Player = player;
            foreach(var def in action.Changes)
            {
                Card card = GameController.CardManager.GetCard(def.Card);
                ECardVisibility current = card.EVisibility;
                if (current != def.TargetVisibility)
                {
                    Add(card, def);
                }
            }
            return this;
        }

        private void Add(Card card, VisibilityChangeDefinition definition)
        {
            //if (definition.Target == EPlayer.Undefined)
            //{
            //    definition.Target = card.Position.Player;
            //}

            if (definition.TargetVisibility.IsVisibleTo(definition.Target, Player))
            {
                CardsToReveal.Add(card);
            }
            else
            {
                if (definition.ShouldBeHiddenForClient)
                {
                    CardsToHide.Add(card.Id);
                }
            }

        }

        protected override void ApplyImplementation()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
