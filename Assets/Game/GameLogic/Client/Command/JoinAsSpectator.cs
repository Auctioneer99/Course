using System.Linq;

public class JoinAsSpectator : IServerCommand
{
    public void Execute(int id, Server server)
    {
        InitializeCommand command = new InitializeCommand(server.Playground.Tiles.Select(pair => pair.Value));
        server.Send(id, command);
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)ServerPackets.JoinAsSpectator);
        return packet;
    }
}
