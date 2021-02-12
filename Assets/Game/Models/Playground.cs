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

    public IDictionary<Vector3, ITile> Tiles => _tiles;
    private IDictionary<Vector3, ITile> _tiles;

    public IEnumerable<IUnit> Units => _tiles.Where(t => t.Value.Unit != null).Select(t => t.Value.Unit);

    public Playground(IEnumerable<ITile> field, int maxPlayers = 2)
    {
        _maxPlayers = maxPlayers;
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

    public ITile TileAt(Vector3 position)
    {
        return _tiles[position];
    }

    public IDictionary<Vector3, ITile> TileConnections(Vector3 position)
    {
        IEnumerable<Vector3> connections = TileAt(position).Connections;
        return (IDictionary<Vector3, ITile>)_tiles.Where(pair => connections.Contains(pair.Key));
    }

    public IDictionary<Vector3, ITile> TileConnections(ITile tile)
    {
        IEnumerable<Vector3> connections = tile.Connections;
        return (IDictionary<Vector3, ITile>)_tiles.Where(pair => connections.Contains(pair.Key));
    }

    public void SetField(IEnumerable<ITile> tiles)
    {
        _tiles = tiles.ToDictionary(value => value.Position);
    }
}
