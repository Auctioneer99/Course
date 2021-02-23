public class PlayerConnected : IClientCommand
{
    private int _id;
    private Player _player;

    public PlayerConnected(int id, Player player)
    {
        _id = id;
        _player = player;
    }

    public void Execute(GameDirector gameDirector)
    {
        gameDirector.TryAddPlayer(_id, _player);
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)ClientPackets.PlayerConnected);
        packet.Write(_player.Name);
        packet.Write((int)_player.Team);
        packet.Write(_player.Hand.Count);
        foreach(var card in _player.Hand)
        {
            packet.Write((int)card.Unit);
        }
        packet.Write(_player.Deck.Count);
        return packet;
    }
}
