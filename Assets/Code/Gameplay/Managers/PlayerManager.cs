using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerManager : AManager, ICensored, IRuntimeDeserializable, IStateObjectCloneable<PlayerManager>
    {
        public Dictionary<EPlayer, Player> Players { get; private set; }

        //public EPlayer PerspectivePlayer { get; private set; }
        public EPlayer LocalUserId => GameController.Network.Role;
        public EPlayer CurrentPlayerId { get; private set; }

        public Player LocalUser => GetPlayer(LocalUserId);
        public Player CurrentPlayer => GetPlayer(CurrentPlayerId);

        public bool HasSpectators => false;

        public PlayerManager(GameController controller) : base(controller)
        {
            int count = controller
                .GameInstance
                .Settings
                .PlayersCount;
            Players = new Dictionary<EPlayer, Player>(count);

            for(int i = 0; i < count; i++)
            {
                Players.Add((EPlayer)(1 << i), null);
            }
        }

        public void Reset()
        {
            foreach(var player in Players)
            {
                player.Value?.Reset();
            }
            Players = new Dictionary<EPlayer, Player>();

            for (int i = 0; i < GameController.GameInstance.Settings.PlayersCount; i++)
            {
                Players.Add((EPlayer)(1 << i), null);
            }
        }

        public Player SetupPlayer(EPlayer eplayer, int connection)
        {
            //Debug.Log("[Player Manager] Incoming player " + eplayer);
            if (Players.TryGetValue(eplayer, out Player p))
            {
                if (p == null)
                {
                    Player player = new Player(this, eplayer, connection);
                    Players[eplayer] = player;

                    GameController.Network.Manager.GetConnection(connection).Role = eplayer;
                    return player;
                }
                else
                {
                    Debug.Log(p.ToString());
                    throw new Exception("Player already set");
                }
            }
            throw new Exception("No slot");
        }

        public void DisconnectPlayer(EPlayer eplayer)
        {
            Debug.Log("[Player Manager] Disconnecting player " + eplayer);
            if (Players.TryGetValue(eplayer, out Player p))
            {
                if (p != null)
                {
                    GameController.EventManager.PlayerDisconnected.Invoke(p);
                    GameController.Network.Manager.GetConnection(p.ConnectionId).Role = EPlayer.Spectators;

                    p.Reset();
                    Players[eplayer] = null;
                }
            }
        }

        public bool AreAllPrepared()
        {
            foreach (var player in Players)
            {
                if (player.Value == null || player.Value.IsPrepared == false)
                {
                    return false;
                }
            }
            return true;
        }

        public void SetupPlayers(EPlayer localuser, Player[] players)
        {
            //LocalUserId = localuser;

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

        public Player GetPlayer(int connection)
        {
            foreach(var p in Players.Values)
            {
                if (p.ConnectionId == connection)
                {
                    return p;
                }
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
            //Debug.Log("[PlayerManager] PlayersCount = " + Players.Count);
            foreach(var player in Players)
            {
                if (player.Value == null || !status.Contains(player.Value.EStatus))
                {
                    //Debug.Log("[PlayerManager] false");
                    return false;
                }
            }
            return true;
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            //LocalUserId = packet.ReadEPlayer();
            CurrentPlayerId = packet.ReadEPlayer();

            int count = controller.GameInstance.Settings.PlayersCount;
            for(int i = 0; i < count; i++)
            {
                EPlayer key = packet.ReadEPlayer();
                Player value = new Player(this, packet);
                Players.Add(key, value);
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(LocalUserId)
                .Write(CurrentPlayerId);

            foreach(var p in Players)
            {
                packet.Write(p.Key)
                    .Write(p.Value);
            }
        }

        public void Censor(EPlayer player)
        {
            //LocalUserId = player;

            foreach(var p in Players.Values)
            {
                p?.Censor(player);
            }
        }

        public PlayerManager Clone(GameController controller)
        {
            PlayerManager pm = new PlayerManager(controller);
            pm.Copy(this, controller);
            return pm;
        }

        public void Copy(PlayerManager other, GameController controller)
        {
            GameController = controller;
            //LocalUserId = other.LocalUserId;
            CurrentPlayerId = other.CurrentPlayerId;

            Players.Clear();
            foreach (var p in other.Players)
            {
                if (p.Value == null)
                {
                    Players.Add(p.Key, null);
                }
                else
                {
                    Players.Add(p.Key, p.Value.Clone(controller));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[PlayerManager]");
            foreach (var p in Players)
            {
                sb.AppendLine(p.Key.ToString());
                if (p.Value == null)
                {
                    sb.AppendLine("No player");
                }
                else
                {
                    sb.AppendLine(p.Value.ToString());
                }
            }
            return sb.ToString();
        }
    }
}
