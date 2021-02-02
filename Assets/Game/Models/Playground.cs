using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections.Immutable;
using System.Numerics;
using System.Linq;

public class Playground
{
    public IEnumerable<Tile> Tiles => _tiles;
    private IImmutableList<Tile> _tiles;

    public IEnumerable<Unit> Units => _tiles.Where(t => t.Unit != null).Select(t => t.Unit);

    public Playground(IEnumerable<Tile> field)
    {
        _tiles = field.ToImmutableList();
    }
}
