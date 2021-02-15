﻿using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class PlaygroundDecorator : IPlaygroundDecorator
{
    private FieldBuilder _builder;
    private Vector3 _position;

    public IPlayground Playground => _playground;
    private IPlayground _playground;

    public IDictionary<Vector3, ITile> Tiles => _playground.Tiles;

    public IEnumerable<IUnit> Units => _playground.Units;


    public PlaygroundDecorator(IPlayground playground, Vector3 position, FieldBuilder builder)
    {
        _playground = playground;
        _position = position;
        _builder = builder;
        BuildField();
    }

    private void BuildField()
    {
        _builder.Build(_position, Tiles.Select(pair => pair.Value.Position));
    }

    public void SetField(IEnumerable<ITile> tiles)
    {
        _playground.SetField(tiles);
        BuildField();
    }

    public ITile TileAt(Vector3 position)
    {
        return _playground.TileAt(position);
    }

    public IDictionary<Vector3, ITile> TileConnections(Vector3 position)
    {
        return _playground.TileConnections(position);
    }

    public IDictionary<Vector3, ITile> TileConnections(ITile tile)
    {
        return _playground.TileConnections(tile);
    }
}
