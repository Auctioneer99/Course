using System.Collections.Generic;

public static class SimplePlaygroundFactory
{
    public static Playground NullPlayground()
    {
        IEnumerable<Tile> field = FieldFactory.NullField();
        Playground playground = new Playground(field);
        return playground;
    }
}
