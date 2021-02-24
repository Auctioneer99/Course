using System;
using System.Collections.Generic;
using System.Numerics;

public static class ClientPacketHandler
{
    public static Dictionary<int, Func<Packet, IClientCommand>> Handlers = new Dictionary<int, Func<Packet, IClientCommand>>
    {
        { (int)ClientPackets.Initialize, InitializeCommand.FromPacket },
        { (int)ClientPackets.PlayerConnected, PlayerConnected.FromPacket },

    };
}
