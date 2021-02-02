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
            Playground pg = PlaygroudFactory.SimplePlayground();
            Tile tile;

            int count = pg.Tiles.Where(t => t.Unit != null).Count();
            Assert.AreEqual(2, count);

            tile = pg.Tiles.First(t => t.Position == new Vector3(1, 0, -1));
            Assert.AreEqual(null, tile.Unit);

            tile = pg.Tiles.First(t => t.Position == new Vector3(0, 0, 0));
            Assert.AreNotEqual(null, tile.Unit);
        }
    }
}
