using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class BattleDeck
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

        public static BattleDeck Default()
        {
            return new BattleDeck();
        }
    }
}
