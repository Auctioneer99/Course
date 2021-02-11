using System;
using System.Collections.Generic;
using System.Numerics;

public static class ClientPacketHandler
{
    public static Dictionary<int, Func<Packet, ICommand>> Handlers = new Dictionary<int, Func<Packet, ICommand>>
    {
        { (int)ClientPackets.Initialize, Initialize },
        { (int)ClientPackets.PlayerConnected, PlayerConnected },

    };

    private static IClientCommand Initialize(Packet packet)
    {
        List<Tile> tiles = new List<Tile>();
        while (packet.UnreadLength() > 0)
        {
            Vector3 pos = packet.ReadVector3();
            int length = packet.ReadInt();
            List<Vector3> connections = new List<Vector3>();
            for (int i = 0; i < length; i++)
            {
                connections.Add(packet.ReadVector3());
            }
            Tile tile = new Tile(pos, connections);
            tiles.Add(tile);
        }
        return new InitializeCommand(tiles);
    }

    private static IClientCommand PlayerConnected(Packet packet)
    {
        return new PlayerConnected(packet.ReadInt(), packet.ReadString(), (Team)packet.ReadInt());
    }
}
