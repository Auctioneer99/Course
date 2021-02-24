using System;

public class MeleeAttackServerCommand : IServerCommand
{
    private Tile _tile;

    public MeleeAttackServerCommand(Tile tile)
    {
        _tile = tile;
    }

    public ServerPackets Command => throw new NotImplementedException();

    public void Execute(int invoker, Server server)
    {

    }

    public Packet ToPacket()
    {
        Packet packet = new Packet(2);
        packet.Write(_tile.Position);
        return packet;
    }
}
