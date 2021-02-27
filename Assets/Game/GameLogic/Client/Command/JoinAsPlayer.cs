using System;
using UnityEngine;

public class JoinAsPlayer : IServerCommand
{
    public ServerPackets Command => ServerPackets.JoinAsPlayer;

    private string _name;
    private string _deckCode;
    private Team _team;


    public JoinAsPlayer(string name, string deckCode, Team team)
    {
        _name = name;
        _deckCode = deckCode;
        _team = team;
    }

    public void Execute(int invoker, Server server)
    {
        if (server.GameDirector.GameState == GameState.WaitingPlayers)
        {
            Player player = new Player(_name, _team);
            bool connected = server.GameDirector.TryAddPlayer(invoker, player);
            if (connected)
            {
                //creating and checking deck;
                server.Clients.Add(new Server.Client(invoker));
                IClientCommand command = new PlayerConnected(invoker, player);
                server.Send(command);
            }
        }
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)Command);
        packet.Write(_name);
        packet.Write(_deckCode);
        packet.Write((int)_team);
        return packet;
    }

    public static JoinAsPlayer FromPacket(Packet packet)
    {
        return new JoinAsPlayer(packet.ReadString(), packet.ReadString(), (Team)packet.ReadInt());
    }
}
