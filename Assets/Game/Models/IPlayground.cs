using System.Collections.Generic;
using System.Numerics;

public interface IPlayground
{
    IDictionary<Vector3, ITile> Tiles { get; }

    IEnumerable<IUnit> Units { get; }

    ITile TileAt(Vector3 position);

    IDictionary<Vector3, ITile> TileConnections(Vector3 position);

    IDictionary<Vector3, ITile> TileConnections(ITile tile);

    void SetField(IEnumerable<ITile> tiles);
}
