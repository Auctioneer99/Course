using System;
using System.Collections.Generic;
using System.Linq;

public class TileProvider
{
    public enum ProviderMode
    {
        ThroughAll,
        Blocking
    }

    private int _range;
    private ProviderMode _mode;
    private Func<Tile, Tile, bool> _condition;

    public TileProvider(int range, ProviderMode mode, Func<Tile, Tile, bool> condition)
    {
        _range = range;
        _mode = mode;
        _condition = condition;
    }

    public IEnumerable<Tile> Provide(Tile origin)
    {
        List<Tile> result = new List<Tile>();

        List<Tile> checkedTiles = new List<Tile>();
        List<Tile> previous = new List<Tile>();
        List<Tile> toCheck = new List<Tile>() { origin };

        for (int i = 0; i < _range; i++)
        {
            result.AddRange(toCheck.Where(t => SatisfiesTheCondition(origin, t)));
            checkedTiles.AddRange(toCheck);

            switch(_mode)
            {
                case ProviderMode.ThroughAll:
                    previous = toCheck;
                    break;
                case ProviderMode.Blocking:
                    previous = toCheck.Where(t => t.Unit == null || t.Unit == origin.Unit).ToList();
                    break;
            }

            toCheck = previous
                .SelectMany(t => t.Connections)
                .Except(checkedTiles).ToList();
        }
        return result;
    }

    public bool SatisfiesTheCondition(Tile origin, Tile tile)
    {
        return _condition(origin, tile);
    }
}
