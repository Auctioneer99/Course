using System;
using System.Linq;

public class GetInitialData : IServerCommand
{
    public ServerPackets Command => ServerPackets.GetInitialData;

    public void Execute(int id, Server server)
    {
        InitializeCommand command = new InitializeCommand(server.GameDirector, new System.Collections.Generic.List<IClientCommand>());
        server.Send(id, command);
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)Command);
        return packet;
    }

    public static GetInitialData FromPacket(Packet packet)
    {
        return new GetInitialData();
    }
}
