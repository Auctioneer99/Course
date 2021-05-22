using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Location : IStateObject<Location>, IRuntimeDeserializable
    {
        public BoardSide BoardSide { get; private set; }

        public ELocation ELocation { get; private set; }

        public List<Card> Cards { get; private set; }
        public int Capacity { get; private set; }

        public Location(BoardSide side, ELocation location, int capacity = -1)
        {
            BoardSide = side;
            ELocation = location;
            Capacity = capacity > 0 ? capacity : 64;
            Cards = new List<Card>(Capacity);
        }

        public virtual void Copy(Location other, GameController controller)
        {
            Cards.Clear();

            foreach(var c in other.Cards)
            {
                //Card card = controller.CardManager.GetCard(c.Id);
                Card card = new Card();
                Cards.Add(card);
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            throw new NotImplementedException();
        }

        public void ToPacket(Packet packet)
        {
            foreach(var c in Cards)
            {
                packet.Write(c.Id);
            }
        }
    }
}
