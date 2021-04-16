using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class LocalListener
    {
        public NetworkManager Manager { get; private set; }
        public bool IsStarted { get; private set; }
        public LocalListener(NetworkManager manager)
        {
            Manager = manager;
        }

        public void Start()
        {
            if (IsStarted)
            {
                throw new Exception("Already started");
            }
            IsStarted = true;
        }

        public void Connect(GameInstance game)
        {
            if (IsStarted)
            {
                Manager.IncomingConnection(game.Controller.Network);
            }
        }
    }
}
