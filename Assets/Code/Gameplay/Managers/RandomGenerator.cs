using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class RandomGenerator
    {
        private Random _random; 

        public RandomGenerator()
        {
            _random = new Random();
        }

        public int Next(int rangeInclusive)
        {
            return _random.Next(rangeInclusive);
        }
    }
}
