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
        if (server.GameDirector.GameState == GameState.WaitingPlayers)
        {
            Player player = new Player(_name, _team);
            bool connected = server.GameDirector.TryAddPlayer(invoker, player);
            if (connected)
            {
                CardFactory factory = new CardFactory();

                for (int i = 0; i < 5; i++)
                {
                    player.Hand.Add(factory.Token());
                }
                server.Clients.Add(new Server.Client(invoker));
                IClientCommand command = new PlayerConnected(invoker, player);
                server.Send(command);
            }
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
