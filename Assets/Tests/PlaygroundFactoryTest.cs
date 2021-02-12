using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Numerics;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlaygroundFactoryTest
    {
        [Test]
        public void PlaygroundFactoryTestSimplePasses()
        {
            IPlayground pg = PlaygroudFactory.SimplePlayground();
            ITile tile;

            int count = pg.Tiles.Where(t => t.Value.Unit != null).Count();
            Assert.AreEqual(2, count);

            tile = pg.TileAt(new Vector3(1, 0, -1));
            Assert.AreEqual(null, tile.Unit);

            tile = pg.TileAt(new Vector3(0, 0, 0));
            Assert.AreNotEqual(null, tile.Unit);
        }
    }
}
