using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Numerics;
using System.Linq;

namespace Tests
{
    public class TileProviderFactoryTest
    {
        [Test]
        public void Move()
        {
            TileProvider move = TileProviderFactory.ForMove(2);
            Playground pg = PlaygroudFactory.SimplePlayground();
            Tile origin = pg.Tiles.First(tile => tile.Position == new Vector3(1, 0, -1));
            IEnumerable<Vector3> expected = new List<Vector3>()
            {
                new Vector3(1, 0, -1),
                new Vector3(1, -1, 0),
            };

            var or = pg.Tiles.First(tile => tile.Position == new Vector3(0, 0, 0));
            Assert.IsTrue(move.SatisfiesTheCondition(origin, or.Connections.First(tile => tile.Position == new Vector3(1, -1, 0))));
            Assert.IsFalse(move.SatisfiesTheCondition(origin, or));
            Assert.IsFalse(move.SatisfiesTheCondition(origin, or.Connections.First(tile => tile.Position == new Vector3(0, 1, -1))));
            Assert.IsTrue(move.SatisfiesTheCondition(origin, or.Connections.First(tile => tile.Position == new Vector3(1, 0, -1))));

            IEnumerable<Tile> options = move.Provide(origin);
            Assert.AreEqual(expected.Count(), options.Select(t => t.Position).Count());
            Assert.IsTrue(expected.Except(options.Select(t => t.Position)).Count() == 0);
        }

        [Test]
        public void MeleeAttack()
        {
            TileProvider attack = TileProviderFactory.ForMeleeAttack();
            Playground pg = PlaygroudFactory.SimplePlayground();
            Tile origin = pg.Tiles.First(tile => tile.Position == new Vector3(1, 0, -1));
            IEnumerable<Vector3> expected = new List<Vector3>()
            {
                new Vector3(0, 1, -1),
                new Vector3(0, 0, 0),
            };

            Assert.IsFalse(attack.SatisfiesTheCondition(origin, pg.Tiles.First(tile => tile.Position == new Vector3(1, -1, 0))));
            Assert.IsTrue(attack.SatisfiesTheCondition(origin, pg.Tiles.First(tile => tile.Position == new Vector3(0, 0, 0))));
            Assert.IsTrue(attack.SatisfiesTheCondition(origin, pg.Tiles.First(tile => tile.Position == new Vector3(0, 1, -1))));
            Assert.IsFalse(attack.SatisfiesTheCondition(origin, pg.Tiles.First(tile => tile.Position == new Vector3(1, 0, -1))));
            
            IEnumerable<Tile> options = attack.Provide(origin);
            Assert.AreEqual(expected.Count(), options.Select(t => t.Position).Count());
            Assert.IsTrue(expected.Except(options.Select(t => t.Position)).Count() == 0);
        }
    }
}
