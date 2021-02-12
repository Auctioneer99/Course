using System.Collections.Generic;
using System.Numerics;

public interface ITile
{
    Team SpawnSide { get; }

    IUnit Unit { get; set; }

    Vector3 Position { get; }

    IEnumerable<Vector3> Connections { get; }
}
