using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;

public class Tile
{
    public Unit Unit {
        get
        {
            return _unit;
        }
        set 
        {
            _unit = value;
        }
    }
    private Unit _unit;

    public Vector3 Position => _position;
    private Vector3 _position;

    public IEnumerable<Tile> Connections => _connections.ToImmutableList();
    private IEnumerable<Tile> _connections;

    public Tile(Vector3 position, IEnumerable<Tile> connections)
    {
        _connections = connections;
        _position = position;
    }
}
