using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Gameplay.Unity
{
    public class MoveListener : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private CardViewManager CardViewManager;
        [SerializeField]
        private BoardView BoardView;

        private GameController _controller;

        void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            _controller.EventManager.CardMoved.VisualEvent.AddListener(OnCardMoved);
            _controller.EventManager.CardsSpawned.VisualEvent.AddListener(OnCardsSpawned);
        }

        public void Detach(GameController game)
        {
            
        }

        public void OnGameEndedCleanup()
        {
            
        }

        private void OnCardMoved(Card card, Position from)
        {
            CardBattleView cardView = CardViewManager.GetCardView(card);

            //LocationView locationView = BoardView.GetLocationView(card.Position);

            if(from == Position.Null)
            {
                cardView.enabled = true;
            }

            SetCardPosition(cardView, card.Position);
        }

        private void OnCardsSpawned(List<Card> cards)
        {
            foreach (var card in cards)
            {
                //Debug.Log(card);
                CardBattleView view = CardViewManager.GetCardView(card);

                view.transform.position = new Vector3(0, 0, 0);
                view.ToggleVisibility(false);

                SetCardPosition(view, card.Position);
            }
        }

        private void SetCardPosition(CardBattleView card, Position position)
        {
            LocationView locView = BoardView.GetLocationView(position);

            if (position.Location.Contains(ELocation.Field | ELocation.Hand))
            {
                card.ToggleVisibility(true);
            }
            else
            {
                card.ToggleVisibility(false);
            }
            Debug.Log("<color=green>Setting position</color>" + "\n" + position.ToString() + "\n" + card.Card.ToString());
            locView?.HandleMove(card);
        }
    }
}
