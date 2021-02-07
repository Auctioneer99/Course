using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InitializeCommand : IClientCommand
{
    IEnumerable<Tile> _tiles;

    public InitializeCommand(IEnumerable<Tile> tiles)
    {
        _tiles = tiles;
    }

    public void Execute(Client client)
    {
        client.Playground.SetField(_tiles);
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)ClientPackets.Initialize);
        foreach(var tile in _tiles)
        {
            packet.Write(tile.Position);
            int connections = tile.Connections.Count();
            packet.Write(connections);
            for(int i = 0; i < connections; i++)
            {
                packet.Write(tile.Connections.ElementAt(i));
            }
        }
        return packet;
    }
}
