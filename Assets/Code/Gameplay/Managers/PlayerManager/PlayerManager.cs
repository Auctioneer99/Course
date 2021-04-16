using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerManager : AManager
    {
        public Dictionary<EPlayer, Player> Players { get; private set; }

        public EPlayer PerspectivePlayer { get; private set; }
        public EPlayer LocalUserId { get; private set; }
        public EPlayer CurrentPlayerId { get; private set; }

        public Player LocalUser => GetPlayer(LocalUserId);
        public Player CurrentPlayer => GetPlayer(CurrentPlayerId);

        public bool HasSpectators => false;

        public PlayerManager(GameController controller) : base(controller)
        {

        }

        public void SetupPlayers(EPlayer perspective, Player[] players)
        {
            PerspectivePlayer = perspective;
            LocalUserId = perspective;

            Players = new Dictionary<EPlayer, Player>(players.Length);
            for (int i = 0; i < players.Length; i++)
            {
                Player player = players[i];
                Players[player.EPlayer] = player;
                GameController.Logger.Log($"PlayerManager {player.EPlayer} Initialized");
            }
        }

        public Player GetPlayer(EPlayer player)
        {
            if (player == EPlayer.Undefined || player == EPlayer.Spectator)
            {
                return null;
            }
            return Players[player];
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
            foreach(var player in Players.Values)
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
