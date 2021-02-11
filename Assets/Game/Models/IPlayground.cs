using System.Collections.Generic;
using System.Numerics;

public interface IPlayground
{
    IDictionary<int, Player> Players { get; }

    IDictionary<Vector3, Tile> Tiles { get; }

    IEnumerable<Unit> Units { get; }

    bool JoinPlayer(int id, Player player);

    Tile TileAt(Vector3 position);

    IDictionary<Vector3, Tile> TileConnections(Vector3 position);

    IDictionary<Vector3, Tile> TileConnections(Tile tile);

    void SetField(IEnumerable<Tile> tiles);
}
