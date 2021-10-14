using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Gameplay.Unity
{
    public class MoveListener : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private CardViewManager CardViewManager = null;
        [SerializeField]
        private BoardView BoardView = null;

        private GameController _controller;

        void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            _controller.EventManager.AfterCardMoved.VisualEvent.AddListener(OnCardMoved);
            _controller.EventManager.AfterCardBattlefieldMoved.VisualEvent.AddListener(OnBattlefieldCardMoved);
            _controller.EventManager.CardsSpawned.VisualEvent.AddListener(OnCardsSpawned);
        }

        public void Detach(GameController game)
        {
            
        }

        public void OnGameEndedCleanup()
        {
            
        }

        private void OnBattlefieldCardMoved(Card card, Position from, List<TileDefinition> path)
        {
            CardBattleView cardView = CardViewManager.GetCardView(card);

            StartCoroutine(BattlefieldMoveCoroutine(cardView, path));
        }

        private IEnumerator BattlefieldMoveCoroutine(CardBattleView cardView, List<TileDefinition> path)
        {
            //_controller pause
            var cardTransform = cardView.transform;

            foreach(var point in path)
            {
                LocationView locView = BoardView.GetLocationView(new Position(point));
                var locPosition = locView.transform.position + new Vector3(0, 10, 0);

                Vector3 startPosition = cardTransform.position;

                for (float progress = 0; progress < 1; progress += Time.deltaTime/2)
                {
                    cardTransform.position = Vector3.Lerp(startPosition, locPosition, progress);
                    yield return null;
                }
            }

            SetCardPosition(cardView, cardView.Card.Position);
        }

        private void OnCardMoved(Card card, Position from)
        {
            CardBattleView cardView = CardViewManager.GetCardView(card);

            //_controller.Logger.Log("CardMoved");
            //_controller.Logger.Log($"Card = {card.Id}, From = {from.ToString()}, To = {card.Position.ToString()}");


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
                if (card.Position.Location == ELocation.Field)
                {
                    Debug.Log("Card Spawned");
                    Debug.Log(card);
                }
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
            //_controller.Logger.Log(locView?.ToString());
            //Debug.Log("<color=green>Setting position</color>" + "\n" + position.ToString() + "\n" + card.Card.ToString());
            locView?.HandleMove(card);
        }
    }
}
