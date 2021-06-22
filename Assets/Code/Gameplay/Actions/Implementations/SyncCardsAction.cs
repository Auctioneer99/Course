using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class SyncCardsAction : AAction, IAuthorityAction, ITargetedAction
    {
        public override EAction EAction => EAction.SyncCards;

        public EPlayer Player => GameController.Network.Manager.GetPlayerTypeFromConnectionId(Connection);

        public List<ushort> CardsToReveal { get; private set; }
        public List<ushort> CardsToHide { get; private set; }

        public Packet CardsToRevealData { get; private set; }

        public int Connection { get; private set; }

        public NetworkTarget Target => NetworkTarget.TargetPlayer;

        public SyncCardsAction()
        {
            CardsToReveal = new List<ushort>();
            CardsToHide = new List<ushort>();
        }

        public SyncCardsAction Initialize(int connectionId, ChangeVisibilityAction action)
        {
            base.Initialize();

            Connection = connectionId;
            CardsToRevealData = new Packet();
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

        public override bool IsValid()
        {
            return  (CardsToHide.Count > 0 || CardsToReveal.Count > 0) && base.IsValid();
        }

        private void Add(Card card, VisibilityChangeDefinition definition)
        {
            if (definition.Target == EPlayer.Undefined)
            {
                definition.Target = card.Position.Player;
            }

            if (definition.TargetVisibility.IsVisibleTo(definition.Target, Player))
            {
                CardsToReveal.Add(card.Id);
                CardsToRevealData.Write(card);
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
            if (GameController.HasAuthority == false)
            {
                foreach (var id in CardsToReveal)
                {
                    Card cardToSync = GameController.CardManager.GetCard(id);
                    cardToSync.FromPacket(GameController, CardsToRevealData);
                }

                foreach(var id in CardsToHide)
                {
                    Card card = GameController.CardManager.GetCard(id);
                    card.Hide();
                }
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
            SyncCardsAction other = copyFrom as SyncCardsAction;
            Connection = other.Connection;
            CardsToHide = new List<ushort>(other.CardsToHide.Count);
            CardsToHide.Copy(other.CardsToHide);
            CardsToReveal = new List<ushort>(other.CardsToReveal.Count);
            CardsToReveal.Copy(other.CardsToReveal);
            CardsToRevealData = new Packet(other.CardsToRevealData.ToArray());
        }
    }
}
