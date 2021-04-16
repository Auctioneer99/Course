using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Settings
    {
        public TimerSettings TimerSettings { get; private set;}

        public Dictionary<EPlayer, PlayerSettings> PlayersSettings { get; private set; }


        public Settings(int playersCount)
        {

            PlayersSettings = new Dictionary<EPlayer, PlayerSettings>(playersCount);
            for(int i = 0; i < playersCount; i++)
            {
                EPlayer index = (EPlayer)(1 << i);
                PlayersSettings[index] = new PlayerSettings(); 
            }
            TimerSettings = new TimerSettings(true);
        }

        public PlayerSettings GetPlayerSettings(EPlayer player)
        {
            return PlayersSettings[player];
        }
    }
}
