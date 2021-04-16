using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerSettings
    {
        public PlayerInfo PlayerInfo;
        public BattleDeck BattleDeck;

        public PlayerSettings()
        {
            PlayerInfo = new PlayerInfo();
            BattleDeck = BattleDeck.Default();
        }
    }
}
