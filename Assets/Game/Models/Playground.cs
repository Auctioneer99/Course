﻿using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Collections.Immutable;

public class Playground
{
    public event Action<Tile, Unit> UnitAdded;
    public event Action<IDictionary<Vector3, Tile>> FieldChanged;
    
    public IDictionary<Vector3, Tile> Tiles => _tiles;
    private IDictionary<Vector3, Tile> _tiles;

    public IEnumerable<Unit> Units => _tiles.Where(t => t.Value.Unit != null).Select(t => t.Value.Unit);

    public Playground(IEnumerable<Tile> field)
    {
        _tiles = field.ToDictionary(tile => tile.Position).ToImmutableDictionary();
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
        FieldChanged?.Invoke(_tiles);
    }

    public Tile TileAtUnit(Unit unit)
    {
        return _tiles.First(pair => pair.Value.Unit == unit).Value;
    }

    public void AddUnit(Unit unit, Tile tile)
    {
        tile.Unit = unit;
        UnitAdded?.Invoke(tile, unit);
    }
}
