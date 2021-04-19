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

        //public EPlayer PerspectivePlayer { get; private set; }
        public EPlayer LocalUserId { get; private set; }
        public EPlayer CurrentPlayerId { get; private set; }

        public Player LocalUser => GetPlayer(LocalUserId);
        public Player CurrentPlayer => GetPlayer(CurrentPlayerId);

        public bool HasSpectators => false;

        public PlayerManager(GameController controller) : base(controller)
        {
            Players = new Dictionary<EPlayer, Player>(controller.GameInstance.Settings.PlayersSettings.Count);
        }

        public void Reset()
        {
            foreach( var player in Players)
            {
                player.Value.Reset();
            }
            Players = new Dictionary<EPlayer, Player>();
        }

        public Player SetupPlayer(EPlayer player)
        {
            if (Players.TryGetValue(player, out Player p) == false)
            {
                Player result = new Player(this, player);
                Players[player] = result;
                return result;
            }
            else
            {
                throw new Exception("Player already set");
            }
        }

        public void SetupPlayers(EPlayer localuser, Player[] players)
        {
            LocalUserId = localuser;

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
            if (EPlayer.Players.Contains(player) == false)
            {
                return null;
            }
            if (Players.TryGetValue(player, out Player p))
            {
                return p;
            }
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
