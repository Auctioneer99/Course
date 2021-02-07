using System;
using System.Collections;
using System.Collections.Generic;

public static class ServerPacketHandler
{
    public static Dictionary<int, Func<Packet, ICommand>> Handlers = new Dictionary<int, Func<Packet, ICommand>>
    {
        { (int)ServerPackets.JoinAsSpectator, JoinAsSpectator },

    };

    private static IServerCommand Print(Packet packet)
    {
        return new PrintCommand(packet.ReadInt(), packet.ReadString());
    }

    private static IServerCommand JoinAsSpectator(Packet packet)
    {
        return new JoinAsSpectator();
    }
}
