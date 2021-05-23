using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Card : IStateObject<Card>, IRuntimeDeserializable
    {
        public int Id { get; private set; }
        public Position Position;

        public void Copy(Card other, GameController controller)
        {

        }

        public void FromPacket(GameController controller, Packet packet)
        {

        }

        public void ToPacket(Packet packet)
        {

        }
    }
}
