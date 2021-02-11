public class JoinAsPlayer : IServerCommand
{
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
        Player player = new Player(_name, _team);
        bool connected = server.Playground.JoinPlayer(invoker, player);
        if (connected)
        {
            IClientCommand command = new PlayerConnected(invoker, _name, _team);
            server.Send(command);
        }
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)ServerPackets.JoinAsPlayer);
        packet.Write(_name);
        packet.Write(_deckCode);
        packet.Write((int)_team);
        return packet;
    }
}
