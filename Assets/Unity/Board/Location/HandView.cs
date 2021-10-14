using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class HandView : LocationView
    {
        private List<CardBattleView> _cards = null;

        public override ELocation ELocation => ELocation.Hand;

        public override void HandleMove(CardBattleView card)
        {
            _cards.Add(card);
            card.transform.parent = transform;
        }

        public override void Initialize(BoardSideView sideView)
        {
            base.Initialize(sideView);

            foreach(var card in Location.Cards)
            {
                CardBattleView cardView = CardViewManager.Instance.GetCardView(card);
                HandleMove(cardView);
            }
            UpdateCardsPosition();
        }

        private void UpdateCardsPosition()
        {
            Vector3 pos = new Vector3(0, 0, 100);
            foreach(var card in _cards)
            {
                card.transform.position = pos;
                pos.z += 100;
            }
        }
    }
}
