using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class CardManager : AManager, IStateObject<CardManager>, IRuntimeDeserializable, ICensored
    {
        public const int DEFAULT_BUFFER_SIZE = 64;
        public const int DEFAULT_BUFFER_INCREMENT = 16;

        private ushort _nextCardId = 0;

        public Card[] Cards { get; private set; }

        public CardManager(GameController controller) : base(controller)
        {
            Cards = new Card[DEFAULT_BUFFER_SIZE];

        }

        public ushort AllocateCardId()
        {
            int cardCount = Cards.Length;
            if (_nextCardId == cardCount)
            {
                Card[] newCards = new Card[cardCount + DEFAULT_BUFFER_INCREMENT];
                Array.Copy(Cards, newCards, cardCount);
                Cards = newCards;
            }

            ushort id = _nextCardId;
            _nextCardId++;
            return id;
        }

        public void Censor(EPlayer player)
        {
            throw new NotImplementedException();
        }

        public void Copy(CardManager other, GameController controller)
        {
            throw new NotImplementedException();
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            throw new NotImplementedException();
        }

        public void ToPacket(Packet packet)
        {
            throw new NotImplementedException();
        }
    }
}
