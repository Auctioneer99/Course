using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class BattlefieldFactorySettings
    {
        public List<PlayerInfluence> PlayersInfluence { get; private set; }

        public BattlefieldFactorySettings(List<PlayerInfluence> pInfluences)
        {
            PlayersInfluence = pInfluences;
        }

        public EPlayer GetOwner(Vector3 rawCoord)
        {
            foreach(var infl in PlayersInfluence)
            {
                if (infl.IsFits(rawCoord))
                {
                    return infl.Player;
                }
            }
            return EPlayer.Neutral;
        }

        public static BattlefieldFactorySettings DefaultSettings()
        {
            InfluenceCondition cond1 = new InfluenceCondition(4, EComparison.Greater);
            InfluenceDirection dir1 = new InfluenceDirection(EDirection.X, cond1);
            PlayerInfluence pInf1 = new PlayerInfluence(EPlayer.Player1, dir1);

            InfluenceCondition cond2 = new InfluenceCondition(-4, EComparison.Less);
            InfluenceDirection dir2 = new InfluenceDirection(EDirection.X, cond2);
            PlayerInfluence pInf2 = new PlayerInfluence(EPlayer.Player2, dir2);

            return new BattlefieldFactorySettings(new List<PlayerInfluence>() { pInf1, pInf2 });
        }
    }
}
