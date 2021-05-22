namespace Gameplay
{
    public class NetworkMessage
    {
        public readonly AConnector Sender;
        public readonly AAction Action;
        public readonly int[] Receivers;

        public NetworkMessage(AConnector sender, AAction action, params int[] receivers)
        {
            Sender = sender;
            Action = action;
            Receivers = receivers;
        }
    }
}
