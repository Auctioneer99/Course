using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay.Unity
{
    public class GameListenerEntry
    {
        public readonly IGameListener Listener;
        public readonly int Priority;

        public bool MarkedForRemove = false;

        private GameController _game;

        public GameListenerEntry(IGameListener listener, int priority)
        {
            Listener = listener;
            Priority = priority;
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            if (game != _game)
            {
                _game = game;
                Listener.Attach(_game, wasJustInitialized);
            }
        }

        public void Detach()
        {
            if (_game != null)
            {
                Listener.Detach(_game);
                _game = null;
            }
        }
    }
}
