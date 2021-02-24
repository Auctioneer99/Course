using System;
using System.Collections;
using System.Collections.Generic;

public static class ServerPacketHandler
{
    public static Dictionary<int, Func<Packet, IServerCommand>> Handlers = new Dictionary<int, Func<Packet, IServerCommand>>
    {
        { (int)ServerPackets.GetInitialData, GetInitialData.FromPacket },
        { (int)ServerPackets.JoinAsPlayer, JoinAsPlayer.FromPacket },

    };
}
