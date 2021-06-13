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

        private GameObject _parent;

        private void Awake()
        {
            _parent = gameObject;
        }

        public void Initialize(Card card)
        {
            Card = card;
        }

        public void ToggleVisibility(bool toDisplay)
        {
            if (toDisplay)
            {
                _parent.SetActive(true);
            }
            else
            {
                _parent.SetActive(false);
            }
        }
    }
}
