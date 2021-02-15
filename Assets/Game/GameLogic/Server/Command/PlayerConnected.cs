public class PlayerConnected : IClientCommand
{
    private int _id;
    private string _name;
    private Team _team;

    public PlayerConnected(int id, string name, Team team)
    {
        _id = id;
        _name = name;
        _team = team;
    }

    public void Execute(Client client)
    {
        if (client.GameDirector.GameState == GameState.WaitingPlayers)
        {
            Player player = new Player(_name, _team);
            client.GameDirector.AddPlayer(_id, player);
        }
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)ClientPackets.PlayerConnected);
        packet.Write(_name);
        packet.Write((int)_team);
        return packet;
    }
}
