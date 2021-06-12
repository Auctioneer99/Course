using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class InfluenceCondition
    {
        public int Value { get; private set; }
        public EComparison Comparison { get; private set; }

        public InfluenceCondition(int value, EComparison comparison)
        {
            Value = value;
            Comparison = comparison;
        }

        public bool IsFits(int value)
        {
            switch(Comparison)
            {
                case EComparison.Equals:
                    return value == Value;
                case EComparison.Greater:
                    return value > Value;
                case EComparison.Less:
                    return value < Value;
                default:
                    return false;
            }
        }
    }
}
