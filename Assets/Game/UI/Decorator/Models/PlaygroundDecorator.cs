using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaygroundDecorator : Playground
{
    private FieldBuilder _builder;

    public PlaygroundDecorator(IEnumerable<Tile> field, FieldBuilder builder) : base(field)
    {
        _builder = builder;
        BuildField();
    }

    private void BuildField()
    {
        _builder.Build(new System.Numerics.Vector3(0, 0, 0), Tiles.Select(pair => pair.Value.Position));
    }

    public override void SetField(IEnumerable<Tile> tiles)
    {
        base.SetField(tiles);
        BuildField();
    }
}
