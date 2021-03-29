using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class GameController
    {
        public bool HasAuthority = true;

        public LoggerManager Logger { get; private set; }
        public ActionFactory ActionFactory { get; private set; }

        public PlayerManager PlayerManager { get; private set; }

        public NotificationManager NotificationManager { get; private set; }
        public AskManager AskManager { get; private set; }
        public ActionManager ActionManager { get; private set; }
        public RequestManager RequestManager { get; private set; }

        public IGateway Network { get; private set; }

        public GameController()
        {
            ActionFactory = new ActionFactory(this);

            PlayerManager = new PlayerManager(this);
            ActionManager = new ActionManager(this);
        }
    }
}
