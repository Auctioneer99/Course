using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerSettings : IDeserializable, IStateObjectCloneable<PlayerSettings>, ICensored
    {
        public PlayerInfo PlayerInfo;
        public BattleDeck BattleDeck;

        public PlayerSettings()
        {
            PlayerInfo = new PlayerInfo();
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
    }
}
