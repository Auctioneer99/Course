using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerVanityData : IDeserializable
    {
        public static PlayerVanityData Default()
        {
            return new PlayerVanityData();
        }

        public void FromPacket(Packet packet)
        {
            
        }

        public void ToPacket(Packet packet)
        {
            
        }
    }
}
