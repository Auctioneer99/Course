public class PrintCommand : ICommand
{
    private int _clientId;
    private string _message;

    public PrintCommand(int clientId, string message)
    {
        _clientId = clientId;
        _message = message;
    }

    public void Execute()
    {
        new UnityLogger("PrintCommand", "white").Log($"id - {_clientId}, message - {_message}");
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)Packets.Print);
        packet.Write(_clientId);
        packet.Write(_message);
        return packet;
    }
}
