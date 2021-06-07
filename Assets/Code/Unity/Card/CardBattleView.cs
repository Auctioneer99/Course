using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class CardBattleView : MonoBehaviour
    {
        public Card Card { get; private set; }

        public void Initialize(Card card)
        {
            Card = card;
        }
    }
}
