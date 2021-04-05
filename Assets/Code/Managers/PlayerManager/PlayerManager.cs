using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerManager : AManager
    {
        public Player[] Players;

        public EPlayer LocalUserId { get; private set; }
        public EPlayer CurrentPlayerId { get; private set; }

        public Player LocalUser => GetPlayer(LocalUserId);
        public Player CurrentPlayer => GetPlayer(CurrentPlayerId);

        public bool HasSpectators => false;

        public PlayerManager(GameController controller) : base(controller)
        {

        }

        public Player GetPlayer(EPlayer player)
        {
            return null;
        }

        public void SetAllPlayersStatus(EPlayerStatus status)
        {
            if (AreAllPlayers(status) == false)
            {
                SetPlayerStatusAction action = GameController.ActionFactory.Create<SetPlayerStatusAction>()
                    .Initialize(EPlayer.Players, status);
                GameController.ActionDistributor.HandleAction(action);
            }
        }

        public bool AreAllPlayers(EPlayerStatus status)
        {
            foreach(var player in Players)
            {
                if (player == null || status.Contains(player.EStatus))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
