using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class CardViewManager : MonoBehaviour, IGameListener
    {
        private const int DEFAULT_BUFFER_SIZE = 64;

        public CardBattleView[] BattleCardViews { get; private set; }
        private GameController _controller;

        public GameObject CardPrefab;

        private void Start()
        {
            BattleCardViews = new CardBattleView[DEFAULT_BUFFER_SIZE];
            App.Instance.Listener.Add(this, int.MinValue);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            //_controller.EventManager.CardsSpawned.VisualEvent.AddListener(OnCardsSpawned);
            //Debug.Log("<color=purple>Attaching</color>");
        }

        private void OnCardsSpawned(List<Card> cards)
        {
            //Debug.Log("<color=purple>OnCardsSpawned</color>");
            //Debug.Log(cards.Count);
            foreach(var card in cards)
            {
                CardBattleView view = GetCardView(card);

                view.transform.position = new Vector3(0, 0, 0);
                view.enabled = false;
            }
        }

        public CardBattleView GetCardView(Card card, bool createIfNull = true)
        {
            CardBattleView view = null;

            if (card != null)
            {
                if (card.Id < BattleCardViews.Length)
                {
                    view = BattleCardViews[card.Id];
                }

                if (view == null && createIfNull)
                {
                    view = RegisterBattleView(card);
                }
            }
            return view;
        }

        private CardBattleView RegisterBattleView(Card card)
        {
            GameObject cardObject = Instantiate(CardPrefab, this.transform);

            CardBattleView view = cardObject.GetComponent<CardBattleView>();

            view.Initialize(card);

            while(BattleCardViews.Length <= card.Id)
            {
                CardBattleView[] newCardViews = new CardBattleView[BattleCardViews.Length + DEFAULT_BUFFER_SIZE];
                Array.Copy(BattleCardViews, newCardViews, BattleCardViews.Length);
                BattleCardViews = newCardViews;
            }
            BattleCardViews[card.Id] = view;

            return view;
        }

        public void Detach(GameController game)
        {
            //
        }

        public void OnGameEndedCleanup()
        {
            throw new NotImplementedException();
        }
    }
}
