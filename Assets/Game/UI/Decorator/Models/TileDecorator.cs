using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDecorator : ITileDecorator
{
    public Team SpawnSide => _tile.SpawnSide;

    public ITile Tile => _tile;
    private ITile _tile;

    public IUnit Unit { get => _tile.Unit; set => _tile.Unit = value; }

    public System.Numerics.Vector3 Position => _tile.Position;

    public IEnumerable<System.Numerics.Vector3> Connections => _tile.Connections;

    public TileDecorator(ITile tile)
    {
        _tile = tile;
    }
}
