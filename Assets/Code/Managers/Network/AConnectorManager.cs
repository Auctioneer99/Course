using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public abstract class AConnectorManager
    {
        protected List<ANetworkConnector> _connectors = new List<ANetworkConnector>();


        public void AddConnector(ANetworkConnector connector)
        {
            _connectors.Add(connector);
        }

        public void Initialize()
        {
            foreach(var connector in _connectors)
            {
                connector.GameController.Network.Connector = connector;
                connector.GameController.Start();
                connector.StartHandlingMessages();
            }
        }

        public virtual void Update()
        {
            foreach(var connector in _connectors)
            {
                if (connector.GameController != null && connector.GameController.Network != null)
                {
                    connector.Update();
                }
            }
        }
    }
}
