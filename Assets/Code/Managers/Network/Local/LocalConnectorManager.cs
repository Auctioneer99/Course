using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
    public class LocalConnectorManager : AConnectorManager
    {
        private delegate void SendDelegate(GameController sender, Packet packet, EPlayer targetPlayer);

        private Dictionary<NetworkTarget, SendDelegate> _targetHandlers;
        private Queue<LocalMessage> _messages = new Queue<LocalMessage>();
        
        public LocalConnectorManager()
        {
            _targetHandlers = new Dictionary<NetworkTarget, SendDelegate>()
            {
                { NetworkTarget.TargetPlayer, SendToTargetPlayer },
                { NetworkTarget.AllOther, SendToAllOthers },
                { NetworkTarget.AllPlayers, SendToAllPlayers },
                { NetworkTarget.Server, SendToServer },
                { NetworkTarget.Spectators, SendToSpectators }
            };
        }

        public void SendCommand(GameController sender, Packet packet, NetworkTarget target, EPlayer targetPlayer)
        {
            LocalMessage message = new LocalMessage(sender, packet, target, targetPlayer);
            _messages.Enqueue(message);
        }

        public override void Update()
        {
            while (_messages.Count > 0)
            {
                UpdateMessages();
            }
        }

        private void UpdateMessages()
        {
            LocalMessage message = _messages.Dequeue();
            _targetHandlers
                .Where(handl => handl.Key == message.Target)
                .First()
                .Value(message.GameController, message.Packet, message.TargetPlayer);
        }

        private void SendToTargetPlayer(GameController controller, Packet packet, EPlayer targetPlayer)
        {
            foreach(var connector in _connectors)
            {
                if (connector.LocalUserId.Contains(targetPlayer))
                {
                    connector.HandlePacket(packet, controller.PlayerManager.LocalUserId);
                }
            }
        }

        private void SendToAllOthers(GameController controller, Packet packet, EPlayer targetPlayer)
        {
            foreach(var connector in _connectors)
            {
                if (connector.GameController != controller)
                {
                    connector.HandlePacket(packet, controller.PlayerManager.LocalUserId);
                }
            }
        }

        private void SendToAllPlayers(GameController controller, Packet packet, EPlayer targetPlayer)
        {
            foreach(var connector in _connectors)
            {
                if (connector.GameController.HasAuthority || controller == connector.GameController)
                {
                    continue;
                }
                if (connector.GameController.PlayerManager.LocalUserId.IsPlayer())
                {
                    connector.HandlePacket(packet, controller.PlayerManager.LocalUserId);
                }
            }
        }

        private void SendToServer(GameController controller, Packet packet, EPlayer targetPlayer)
        {
            _connectors
                .Where(connector => connector.GameController.PlayerManager.LocalUserId == EPlayer.Undefined)
                .First()
                .HandlePacket(packet, controller.PlayerManager.LocalUserId);
        }

        private void SendToSpectators(GameController controller, Packet packet, EPlayer targetPlayer)
        {
            foreach( var connector in _connectors)
            {
                if(connector.GameController.PlayerManager.LocalUserId.IsSpectator())
                {
                    connector.HandlePacket(packet, controller.PlayerManager.LocalUserId);
                }
            }
        }
    }
}
