using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Player
    {
        public PlayerManager PlayerManager { get; private set; }
        public GameController GameController => PlayerManager.GameController;

        public EPlayer EPlayer { get; private set; }
        public EPlayerStatus EStatus
        {
            get
            {
                return _eStatus;
            }
            set
            {
                if (_eStatus != value)
                {
                    _eStatus = value;
                    GameController.EventManager.OnPlayerStatusChanged.Invoke(this);
                }
            }
        }
        private EPlayerStatus _eStatus;

        public PingStatus PingStatus { get; private set; }

        public Player()
        {

        }
    }
}
