using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class InitializeCommand : IClientCommand
{
    public ClientPackets Command => ClientPackets.Initialize;

    private GameDirector _director;
    private List<IClientCommand> _commands;

    public InitializeCommand(GameDirector director, List<IClientCommand> commands)
    {
        _director = director;
        _commands = commands;
    }

    public void Execute(GameDirector gameDirector)
    {
        gameDirector.Copy(_director);
    }

    public Packet ToPacket()
    {
        Packet packet = new Packet((int)Command);

        packet.Write(_director.Playground.Tiles.Count);
        foreach (var tile in _director.Playground.Tiles.Values)
        {
            packet.Write(tile.Position);
            packet.Write((int)tile.SpawnSide);
            bool hasUnit = tile.Unit != null;
            packet.Write(hasUnit);
            if (hasUnit)
            {
                packet.Write((int)tile.Unit.Team);
                packet.Write(tile.Unit.Attack.Origin);
                packet.Write(tile.Unit.Health.Current.Amount);
                packet.Write(tile.Unit.Health.Max.Origin);
                packet.Write(tile.Unit.Moves.Current.Amount);
                packet.Write(tile.Unit.Moves.Max.Origin);
                packet.Write(tile.Unit.Initiative.Origin);
            }
        }

        packet.Write(_director.Round);
        packet.Write((int)_director.GameState);
        packet.Write(_director.PlayersCount);

        packet.Write(_director.Players.Count);
        foreach (var p in _director.Players)
        {
            Packet command = new PlayerConnected(p.Key, p.Value).ToPacket();
            packet.Write(command.Length());
            packet.Write(command.ToArray());
            /*
            packet.Write(p.Key);
            packet.Write(p.Value.Name);
            packet.Write((int)p.Value.Team);
            packet.Write(p.Value.Mana);
            packet.Write(p.Value.Hand.Count);
            packet.Write(p.Value.Deck.Count);*/
        }
        return packet;
    }

    public static InitializeCommand FromPacket(Packet packet)
    {
        List<Tile> tiles = new List<Tile>();
        int tileCount = packet.ReadInt();
        for (int i = 0; i < tileCount; i++)
        {
            Vector3 position = packet.ReadVector3();
            Team spawnSide = (Team)packet.ReadInt();
            Tile tile = new Tile(position, FieldFactory.GetConnections(position, tiles.Select(t => t.Position)), spawnSide);
            bool hasUnit = packet.ReadBool();
            if (hasUnit)
            {
                Team team = (Team)packet.ReadInt();
                int attack = packet.ReadInt();
                int curHealth = packet.ReadInt();
                int maxHealth = packet.ReadInt();
                int curMoves = packet.ReadInt();
                int maxMoves = packet.ReadInt();
                int initiative = packet.ReadInt();
                Unit unit = new Unit(team, maxHealth, attack, maxMoves, initiative, new List<AbilityData>());
                unit.Health.Current.Amount = curHealth;
                unit.Moves.Current.Amount = curMoves;
                tile.Unit = unit;
            }
            tiles.Add(tile);
        }

        Playground playground = new Playground(tiles);

        int round = packet.ReadInt();
        GameState state = (GameState)packet.ReadInt();
        int playerCount = packet.ReadInt();

        GameDirector director = new GameDirector(playground, playerCount);
        director.GameState = state;

        int currentPlayers = packet.ReadInt();
        for (int i = 0; i < currentPlayers; i++)
        {
            int length = packet.ReadInt();
            Packet player = new Packet(packet.ReadBytes(length));
            PlayerConnected com = PlayerConnected.FromPacket(player);
            com.Execute(director);
            /*
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
            director.Players[id] = player;
            */
        }
        return new InitializeCommand(director, new List<IClientCommand>());
    }
}
