using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class RuntimeBattleDeck : BattleDeck
    {

        public RuntimeBattleDeck() { }

        public RuntimeBattleDeck(GameController controller, BattleDeck deck)
        {
            Hero = deck.Hero;
            Cards = deck.Cards;
        }

    }
}
