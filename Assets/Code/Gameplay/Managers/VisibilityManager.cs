using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class VisibilityManager : AManager
    {
        public VisibilityManager(GameController controller): base(controller)
        {

        }

        public void ChangeVisibility(IVisibilityChangeRequester requester)
        {
            if (GameController.HasAuthority)
            {
                ChangeVisibilityAction visibilityAction = GameController.ActionFactory.Create<ChangeVisibilityAction>().Initialize();
                requester.AddCardsToChangeVisibility(visibilityAction);
                if (visibilityAction.Changes.Count > 0)
                {
                    foreach(var player in GameController.PlayerManager.Players)
                    {
                        SyncCardsAction syncAction = GameController.ActionFactory.Create<SyncCardsAction>().Initialize(player.Key, visibilityAction);
                        GameController.ActionDistributor.HandleAction(syncAction);
                    }
                    GameController.ActionDistributor.HandleAction(visibilityAction);
                }
            }
        }

        public static ECardVisibility GetTargetVisibilityWhenMoving(Card card, Position from, Position to)
        {
            if (to.Location.Contains(ELocation.Field | ELocation.Graveyard))
            {
                return ECardVisibility.All;
            }

            if (to.Location.Contains(ELocation.Hand | ELocation.Mulligan | ELocation.Deck))
            {
                return ECardVisibility.Owner;
            }

            return ECardVisibility.Noone;
        }

        public static ECardVisibility GetVisibility(Card card)
        {
            if (card.Definition == CardDefinition.Unknown)
            {
                return ECardVisibility.Noone;
            }
            return GetVisibility(card.Position);
        }

        public static ECardVisibility GetVisibility(Position position)
        {
            ELocation location = position.Location;

            switch (location)
            {
                case ELocation.Deck:
                    return ECardVisibility.Owner;
                case ELocation.Field:
                    return ECardVisibility.All;
                case ELocation.Graveyard:
                    return ECardVisibility.All;
                case ELocation.Hand:
                    return ECardVisibility.Owner;
                case ELocation.Mulligan:
                    return ECardVisibility.Owner;
                case ELocation.Spawn:
                    return ECardVisibility.Noone;
                default:
                    throw new Exception("Unsupported Location");
            }
        }
    }
}
