using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerInfo : IDeserializable, IStateObjectCloneable<PlayerInfo>
    {
        public string Name = string.Empty;

        public PlayerVanityData Vanity = PlayerVanityData.Default();

        public PlayerInfo()
        {

        }

        public PlayerInfo(string name, PlayerVanityData vanity)
        {
            Name = name;
            Vanity = vanity;
        }

        public PlayerInfo(Packet packet)
        {
            FromPacket(packet);
        }

        public void FromPacket(Packet packet)
        {
            Name = packet.ReadString();
            Vanity = new PlayerVanityData(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Name)
                .Write(Vanity);
        }

        public PlayerInfo Clone(GameController controller)
        {
            PlayerInfo info = new PlayerInfo();
            info.Copy(this, controller);
            return this;
        }

        public void Copy(PlayerInfo other, GameController controller)
        {
            Name = other.Name;
            Vanity = other.Vanity.Clone(controller);
        }
    }
}
