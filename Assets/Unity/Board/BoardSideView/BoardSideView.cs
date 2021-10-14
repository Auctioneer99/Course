using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class BoardSideView : MonoBehaviour, IGameListener
    {
        protected GameController _controller;

        public EPlayer EPlayer { get; private set; }
        public BoardView BoardView { get; private set; }
        public BoardSide BoardSide { get; private set; }


        public void Initialize(BoardView boardView, EPlayer player)
        {
            BoardView = boardView;
            EPlayer = player;

            App.Instance.Listener.Add(this);
        }

        public virtual void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;
            BoardSide = game.BoardManager.GetBoardSide(EPlayer);
        }

        public virtual void Detach(GameController game)
        {
        }

        public virtual void OnGameEndedCleanup()
        {
        }
    }
}
