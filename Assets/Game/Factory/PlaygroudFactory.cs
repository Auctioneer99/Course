using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using System.Numerics;

public static class PlaygroudFactory
{
    public static IPlayground SimplePlayground()
    {
        IEnumerable<ITile> field = FieldFactory.SimpleField2();
        IPlayground pg = new Playground(field);
        ITile tile;

        //tile = pg.TileAt(new Vector3(0, 0, 0));
        //tile.Unit = UnitFactory.Warrior();

        //tile = pg.TileAt(new Vector3(0, 1, -1));
        //tile.Unit = UnitFactory.Warrior();

        return pg;
    }
}
