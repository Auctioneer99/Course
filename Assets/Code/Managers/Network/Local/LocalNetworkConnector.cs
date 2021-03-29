namespace Gameplay
{
    public class LocalNetworkConnector : ANetworkConnector
    {
        private LocalConnectorManager _manager;

        public LocalNetworkConnector(LocalConnectorManager manager, GameController controller) : base(controller)
        {
            _manager = manager;
        }

        public override void Send(Packet packet, NetworkTarget target, EPlayer targetPlayer)
        {
            _manager.SendCommand(GameController, packet, target, targetPlayer);
        }
    }
}
