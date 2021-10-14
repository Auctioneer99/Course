using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class InputFlowConnector
    {
        public IFlowNode Owner { get; private set; }

        public InputFlowConnector(IFlowNode node)
        {
            Owner = node;
        }
    }

    public class OutputFlowConnector
    {
        public InputFlowConnector ConnectedFlow { get; set; }
    }
}
