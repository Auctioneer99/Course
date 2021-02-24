using System.Collections.Generic;

public class PlayerConnected : IClientCommand
{
    public ClientPackets Command => ClientPackets.PlayerConnected;

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
        Packet packet = new Packet((int)Command);

        packet.Write(_id);
        packet.Write(_player.Name);
        packet.Write((int)_player.Team);
        packet.Write(_player.Mana);
        packet.Write(_player.Hand.Count);
        packet.Write(_player.Deck.Count);

        return packet;
    }

    public static PlayerConnected FromPacket(Packet packet)
    {
        int id = packet.ReadInt();
        string name = packet.ReadString();
        Team team = (Team)packet.ReadInt();

        int mana = packet.ReadInt();
        int handCount = packet.ReadInt();
        int deckCount = packet.ReadInt();

        Player player = new Player(name, team);

        player.Mana = mana;
        player.Hand = new List<Card>(handCount);
        player.Deck = new List<Card>(deckCount);

        return new PlayerConnected(id, player);
    }
}
