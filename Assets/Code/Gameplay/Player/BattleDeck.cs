using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class BattleDeck : IDeserializable, IStateObjectCloneable<BattleDeck>, ICensored
    {
        public string Name = string.Empty;
        public CardDefinition Hero;
        public List<CardDefinition> Cards = new List<CardDefinition>(30);

        public BattleDeck()
        {

        }

        public BattleDeck(string name, CardDefinition hero, List<CardDefinition> cards)
        {
            Name = name;
            Hero = hero;
            Cards = cards;
        }

        public BattleDeck(Packet packet)
        {
            FromPacket(packet);
        }

        public static BattleDeck Default()
        {
            return new BattleDeck();
        }

        public void FromPacket(Packet packet)
        {
            
        }

        public void ToPacket(Packet packet)
        {
            
        }

        public void Censor(EPlayer player)
        {
            
        }

        public BattleDeck Clone(GameController controller)
        {
            return this;
        }

        public void Copy(BattleDeck other, GameController controller)
        {
            
        }
    }
}
