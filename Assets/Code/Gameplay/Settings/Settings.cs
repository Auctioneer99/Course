using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class Settings : IDeserializable, IStateObjectCloneable<Settings>, ICensored
    {
        public TimerSettings TimerSettings { get; private set;}

        public int PlayersCount { get; private set; }
        public Dictionary<EPlayer, PlayerSettings> PlayersSettings { get; private set; }

        public Settings()
        {

        }

        public Settings(int playersCount)
        {
            PlayersCount = playersCount;
            PlayersSettings = new Dictionary<EPlayer, PlayerSettings>(playersCount);
            for(int i = 0; i < playersCount; i++)
            {
                EPlayer index = (EPlayer)(1 << i);
                PlayersSettings[index] = new PlayerSettings();
            }
            TimerSettings = new TimerSettings(true);
        }

        public Settings(Packet packet)
        {
            FromPacket(packet);
        }

        public PlayerSettings GetPlayerSettings(EPlayer player)
        {
            return PlayersSettings[player];
        }

        public void FromPacket(Packet packet)
        {
            PlayersCount = packet.ReadInt();
            PlayersSettings = new Dictionary<EPlayer, PlayerSettings>(PlayersCount);

            for(int i = 0; i < PlayersCount; i++)
            {
                EPlayer id = packet.ReadEPlayer();
                bool isConnected = packet.ReadBool();
                PlayerSettings player = null;
                if (isConnected)
                {
                    player = new PlayerSettings(packet);
                }
                PlayersSettings[id] = player;
            }
            TimerSettings = new TimerSettings(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(PlayersCount);
            Debug.Log("Serializing settings");
            foreach(var player in PlayersSettings)
            {
                Debug.Log(player.Key);
                packet.Write(player.Key);
                bool isConnected = player.Value != null;
                packet.Write(isConnected);
                if (isConnected)
                {
                    packet.Write(player.Value);
                }
            }
            packet.Write(TimerSettings);
        }
        public Settings Clone(GameController controller)
        {
            Settings settings = new Settings();
            settings.Copy(this, controller);
            return settings;
        }

        public void Copy(Settings other, GameController controller)
        {
            PlayersCount = other.PlayersCount;
            PlayersSettings = other.PlayersSettings.Clone(controller);
            TimerSettings = other.TimerSettings.Clone(controller);
        }

        public void Censor(EPlayer player)
        {
            foreach(var set in PlayersSettings)
            {
                if (player.Contains(set.Key) == false)
                {
                    set.Value?.Censor(player);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Settings]");
            sb.Append($"\n\tPlayersCount = {PlayersCount}");
            foreach(var p in PlayersSettings)
            {
                sb.Append($"\n\tPlace for = {p.Key}");
                sb.Append($"\n\t{p.Value}");
            }
            return sb.ToString();
        }
    }
}
