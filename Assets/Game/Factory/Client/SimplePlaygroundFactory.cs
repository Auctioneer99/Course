using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SimplePlaygroundFactory : IPlaygroundFactory
{
    public SimplePlaygroundFactory()
    {

    }

    public IPlayground NullPlayground()
    {
        IEnumerable<Tile> field = FieldFactory.NullField();
        IPlayground playground = new Playground(field);
        return playground;
    }
}
