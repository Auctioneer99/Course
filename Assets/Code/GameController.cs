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

        public ActionDistributor ActionDistributor { get; private set; }
        public RequestHolder RequestHolder { get; private set; }
        public ANetworkConnector Network { get; private set; }
        public EventManager EventManager { get; private set; }

        public GameController()
        {
            Logger = new LoggerManager(this);
            ActionFactory = new ActionFactory(this);

            PlayerManager = new PlayerManager(this);
            ActionDistributor = new ActionDistributor(this);
            RequestHolder = new RequestHolder(this);

            EventManager = new EventManager(this);
        }

        public void Start()
        {

        }

        public void Update()
        {

        }
    }
}
