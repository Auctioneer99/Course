using System;
using System.Collections;
using System.Collections.Generic;

public static class ServerPacketHandler
{
    public static Dictionary<int, Func<Packet, ICommand>> Handlers = new Dictionary<int, Func<Packet, ICommand>>
    {
        { (int)ServerPackets.GetInitialData, GetInitialData },
        { (int)ServerPackets.JoinAsPlayer, JoinAsPlayer },

    };

    private static IServerCommand GetInitialData(Packet packet)
    {
        return new GetInitialData();
    }

    private static IServerCommand JoinAsPlayer(Packet packet)
    {
        return new JoinAsPlayer(packet.ReadString(), packet.ReadString(), (Team)packet.ReadInt());
    }
}
