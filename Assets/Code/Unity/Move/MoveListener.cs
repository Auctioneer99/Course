using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Unity
{
    public class MoveListener : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private CardViewManager CardViewManager;
        [SerializeField]
        private LocationViewManager LocationViewManager;

        private GameController _controller;

        void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            game.EventManager.CardMoved.VisualEvent.AddListener(OnCardMoved);
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

            LocationView locationView = LocationViewManager.GetLocationView(card.Position);


        }
    }
}
