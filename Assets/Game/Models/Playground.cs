using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Collections.Immutable;

public class Playground : IPlayground
{
    public int MaxPlayers => _maxPlayers;
    private int _maxPlayers;

    public IDictionary<int, Player> Players => _players;
    public IDictionary<int, Player> _players;

    public IDictionary<Vector3, Tile> Tiles => _tiles;
    private IDictionary<Vector3, Tile> _tiles;

    public IEnumerable<Unit> Units => _tiles.Where(t => t.Value.Unit != null).Select(t => t.Value.Unit);

    public Playground(IEnumerable<Tile> field, int maxPlayers = 2)
    {
        _maxPlayers = 2;
        _tiles = field.ToDictionary(tile => tile.Position).ToImmutableDictionary();
        _players = new Dictionary<int, Player>();
    }

    public bool JoinPlayer(int id, Player player)
    {
        if (_players.Count >= _maxPlayers)
        {
            return false;
        }
        foreach (var pair in _players)
        {
            if (pair.Value.Team == player.Team)
            {
                return false;
            }
        }

        _players[id] = player;
        return true;
    }

    public Tile TileAt(Vector3 position)
    {
        return _tiles[position];
    }

    public IDictionary<Vector3, Tile> TileConnections(Vector3 position)
    {
        IEnumerable<Vector3> connections = TileAt(position).Connections;
        return (IDictionary<Vector3, Tile>)_tiles.Where(pair => connections.Contains(pair.Key));
    }

    public IDictionary<Vector3, Tile> TileConnections(Tile tile)
    {
        IEnumerable<Vector3> connections = tile.Connections;
        return (IDictionary<Vector3, Tile>)_tiles.Where(pair => connections.Contains(pair.Key));
    }

    public void SetField(IEnumerable<Tile> tiles)
    {
        _tiles = tiles.ToDictionary(value => value.Position);
    }
}
