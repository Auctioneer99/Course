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
        public BoardSideView BoardSideView { get; protected set; }
        public Location Location { get; protected set; }
        public abstract ELocation ELocation { get; }

        protected GameController _controller;

        public virtual void Initialize(BoardSideView sideView)
        {
            BoardSideView = sideView;
            Location = BoardSideView.BoardSide.GetLocation(ELocation);
        }

        public virtual void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;
        }

        public void Detach(GameController game)
        {
            
        }

        public void OnGameEndedCleanup()
        {
            
        }
    }
}
