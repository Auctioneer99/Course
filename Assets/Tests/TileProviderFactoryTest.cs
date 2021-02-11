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
            IPlayground pg = PlaygroudFactory.SimplePlayground();
            Tile origin = pg.TileAt(new Vector3(1, 0, -1));
            IEnumerable<Vector3> expected = new List<Vector3>()
            {
                new Vector3(1, 0, -1),
                new Vector3(1, -1, 0),
            };

            var or = pg.TileAt(new Vector3(0, 0, 0));

            IEnumerable<Tile> options = move.Provide(origin, pg);
            Assert.AreEqual(expected.Count(), options.Select(t => t.Position).Count());
            Assert.IsTrue(expected.Except(options.Select(t => t.Position)).Count() == 0);
        }

        [Test]
        public void MeleeAttack()
        {
            TileProvider attack = TileProviderFactory.ForMeleeAttack();
            IPlayground pg = PlaygroudFactory.SimplePlayground();
            Tile origin = pg.TileAt(new Vector3(1, 0, -1));
            IEnumerable<Vector3> expected = new List<Vector3>()
            {
                new Vector3(0, 1, -1),
                new Vector3(0, 0, 0),
            };
            
            IEnumerable<Tile> options = attack.Provide(origin, pg);
            Assert.AreEqual(expected.Count(), options.Select(t => t.Position).Count());
            Assert.IsTrue(expected.Except(options.Select(t => t.Position)).Count() == 0);
        }
    }
}
