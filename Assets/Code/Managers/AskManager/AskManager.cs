using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class AskManager : AManager
    {
        private List<AAsk> _asks;

        public AskManager(GameController controller) : base(controller) 
        {
            _asks = new List<AAsk>();
        }

        public void Add(AAsk ask)
        {
            _asks.Add(ask);
        }
    }
}
