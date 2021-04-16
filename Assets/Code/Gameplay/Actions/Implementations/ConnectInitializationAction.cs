using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class ConnectInitializationAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.ConnectInitialization;

        public int ConnectionId { get; private set; }

        public ConnectInitializationAction Initialize(int connection)
        {
            base.Initialize();
            ConnectionId = connection;
            return this;
        }

        protected override void ApplyImplementation()
        {
            throw new NotImplementedException();
        }

        protected override void AttributesFrom(Packet packet)
        {
            ConnectionId = packet.ReadInt();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(ConnectionId);
        }
    }
}
