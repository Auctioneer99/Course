using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public abstract class BoardSideView : MonoBehaviour, IGameListener
    {
        protected GameController _controller;

        public Player Player { get; protected set; }

        public BoardSide BoardSide { get; private set; }

        public virtual LocationView GetLocationView(Position position)
        {
            return null;
        }

        public virtual void Attach(GameController game, bool wasJustInitialized)
        {
        }

        public virtual void Detach(GameController game)
        {
        }

        public virtual void OnGameEndedCleanup()
        {
        }
    }
}
