using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using System.Numerics;

public static class PlaygroudFactory
{
    public static Playground SimplePlayground()
    {
        IEnumerable<Tile> field = FieldFactory.SimpleField2();
        Playground pg = new Playground(field);
        Tile tile;

        tile = pg.Tiles.First(t => t.Position == new Vector3(0, 0, 0));
        tile.Unit = UnitFactory.Warrior();

        tile = pg.Tiles.First(t => t.Position == new Vector3(0, 1, -1));
        tile.Unit = UnitFactory.Warrior();

        return pg;
    }
}
