using System.Collections;
using System.Collections.Generic;

public static class PacketHandler
{
    public static ICommand Print(Packet packet)
    {
        return new PrintCommand(packet.ReadInt(), packet.ReadString());
    }
}
