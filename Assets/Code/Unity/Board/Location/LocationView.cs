using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public abstract class LocationView : MonoBehaviour, IGameListener
    {
        public BoardSideView BoardSideView { get; private set; }
        public Location Location { get; private set; }
        public ELocation ELocation;

        private GameController _controller;

        public virtual void Prepare(BoardSideView sideView)
        {
            BoardSideView = sideView;

            App.Instance.Listener.Add(this);
        }

        public virtual void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;
            EPlayer player = BoardSideView.Player.EPlayer;
            BoardSide side = _controller.BoardManager.GetBoardSide(player);
            Location = side.GetLocation(ELocation);
        }

        public void Detach(GameController game)
        {
            
        }

        public void OnGameEndedCleanup()
        {
            
        }
    }
}
