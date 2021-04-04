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

        public EPlayer EPlayer { get; private set; }
        public EPlayerStatus EStatus { get; private set; }

        public PingStatus PingStatus { get; private set; }

        public Player()
        {

        }
    }
}
