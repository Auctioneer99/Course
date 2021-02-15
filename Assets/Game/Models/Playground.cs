using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Collections.Immutable;

public class Playground : IPlayground
{
    public IDictionary<Vector3, ITile> Tiles => _tiles;
    private IDictionary<Vector3, ITile> _tiles;

    public IEnumerable<IUnit> Units => _tiles.Where(t => t.Value.Unit != null).Select(t => t.Value.Unit);

    public Playground(IEnumerable<ITile> field)
    {
        _tiles = field.ToDictionary(tile => tile.Position).ToImmutableDictionary();
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
