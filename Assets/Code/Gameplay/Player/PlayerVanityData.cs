using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerVanityData : IDeserializable, IStateObjectCloneable<PlayerVanityData>
    {
        public PlayerVanityData()
        {

        }

        public static PlayerVanityData Default()
        {
            return new PlayerVanityData();
        }

        public PlayerVanityData(Packet packet)
        {
            FromPacket(packet);
        }

        public PlayerVanityData Clone()
        {
            PlayerVanityData data = new PlayerVanityData();
            data.Copy(this);
            return data;
        }

        public void Copy(PlayerVanityData other)
        {

        }

        public void FromPacket(Packet packet)
        {

        }

        public void ToPacket(Packet packet)
        {

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[PlayerVanityData]");
            return sb.ToString();
        }
    }
}
