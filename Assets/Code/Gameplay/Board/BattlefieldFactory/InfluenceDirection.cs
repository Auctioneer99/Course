using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class InfluenceDirection
    {
        public InfluenceCondition Condition { get; private set; }

        public EDirection Direction { get; private set; }

        public InfluenceDirection(EDirection direction, InfluenceCondition condition)
        {
            Direction = direction;
            Condition = condition;
        }

        public bool IsFits(Vector3 rawCoord)
        {
            switch(Direction)
            {
                case EDirection.X:
                    return Condition.IsFits((int)rawCoord.X);
                case EDirection.Y:
                    return Condition.IsFits((int)rawCoord.Y);
                case EDirection.Z:
                    return Condition.IsFits((int)rawCoord.Z);
                default:
                    return false;
            }
        }
    }
}
