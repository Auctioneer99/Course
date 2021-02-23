using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDecorator
{
    public Team SpawnSide => _tile.SpawnSide;

    public Tile Tile => _tile;
    private Tile _tile;

    public Unit Unit { get => _tile.Unit; set => _tile.Unit = value; }

    public System.Numerics.Vector3 Position => _tile.Position;

    public IEnumerable<System.Numerics.Vector3> Connections => _tile.Connections;

    public TileDecorator(Tile tile)
    {
        _tile = tile;
    }
}
