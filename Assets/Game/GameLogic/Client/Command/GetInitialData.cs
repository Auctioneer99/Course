using System.Linq;

public class GetInitialData : IServerCommand
{
    public void Execute(int id, Server server)
    {
        InitializeCommand command = new InitializeCommand(server.GameDirector.Playground.Tiles.Select(pair => pair.Value));
        server.Send(id, command);
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)ServerPackets.GetInitialData);
        return packet;
    }
}
