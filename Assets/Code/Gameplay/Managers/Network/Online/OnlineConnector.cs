using WebSocketSharp;

using UnityEngine;

namespace Gameplay
{
    public class OnlineConnector : AConnector
    {
        public WebSocket Socket { get; private set; }

        private Logger _logger;

        public OnlineConnector(WebSocket socket, Logger logger)
        {
            Socket = socket;
            logger = _logger;
            //connecting to db read data
        }

        public OnlineConnector(WebSocket socket, string sub, Logger logger)
        {
            Socket = socket;
            //connecting to db read data
            logger = _logger;
        }

        public override void GetDecks()
        {

        }

        public override void HandleMessage(AConnector sender, AAction action)
        {
            Packet packet = new Packet();
            action.ToPacket(packet);
            Socket.SendAsync(packet.ToArray(), null);
        }
    }
}
