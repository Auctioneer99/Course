using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerInfo : IDeserializable
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

        public void FromPacket(Packet packet)
        {
            Name = packet.ReadString();
            Vanity.FromPacket(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Name)
                .Write(Vanity);
        }
    }
}
