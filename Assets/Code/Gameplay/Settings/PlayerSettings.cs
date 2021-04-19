using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerSettings : IDeserializable, IStateObjectCloneable<PlayerSettings>, ICensored
    {
        public EPlayer Player;
        public PlayerInfo PlayerInfo;
        public BattleDeck BattleDeck;

        public PlayerSettings()
        {
            Player = EPlayer.Undefined;
            PlayerInfo = new PlayerInfo();
            BattleDeck = BattleDeck.Default();
        }

        public PlayerSettings(EPlayer player)
        {
            Player = player;
            PlayerInfo = new PlayerInfo();
            BattleDeck = BattleDeck.Default();
        }

        public PlayerSettings(EPlayer player, PlayerInfo info)
        {
            Player = player;
            PlayerInfo = info;
            BattleDeck = BattleDeck.Default();
        }

        public PlayerSettings(Packet packet)
        {
            FromPacket(packet);
        }

        public void Censor(EPlayer player)
        {
            BattleDeck.Censor(player);
        }

        public PlayerSettings Clone(GameController controller)
        {
            PlayerSettings settings = new PlayerSettings();
            settings.Copy(this, controller);
            return settings;
        }

        public void Copy(PlayerSettings other, GameController controller)
        {
            PlayerInfo = other.PlayerInfo.Clone(controller);
            BattleDeck = other.BattleDeck.Clone(controller);
        }

        public void FromPacket(Packet packet)
        {
            PlayerInfo = new PlayerInfo(packet);
            BattleDeck = new BattleDeck(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(PlayerInfo)
                .Write(BattleDeck);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[PlayerSettings]");
            sb.Append($"\n\t{Player}");
            sb.Append($"\n\t{PlayerInfo}");
            sb.Append($"\n\t{BattleDeck}");
            return sb.ToString();
        }
    }
}
