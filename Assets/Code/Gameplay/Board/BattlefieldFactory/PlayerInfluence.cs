using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerInfluence
    {
        public EPlayer Player { get; private set; }

        public List<InfluenceDirection> DirectionConditions { get; private set; }

        public PlayerInfluence(EPlayer player)
        {
            Player = player;
            DirectionConditions = new List<InfluenceDirection>();
        }

        public PlayerInfluence(EPlayer player, InfluenceDirection condition)
        {
            Player = player;
            DirectionConditions = new List<InfluenceDirection>();
            DirectionConditions.Add(condition);
        }

        public PlayerInfluence(EPlayer player, List<InfluenceDirection> conditions)
        {
            Player = player;
            DirectionConditions = conditions;
        }

        public bool IsFits(Vector3 rawCoord)
        {
            if (DirectionConditions.Count == 0)
            {
                return false;
            }

            foreach(var cond in DirectionConditions)
            {
                if( cond.IsFits(rawCoord) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
