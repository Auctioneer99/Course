using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Numerics;

namespace Tests
{
    public class FieldFactoryTest
    {
        [Test]
        public void RadialFieldTileCount()
        {
            IEnumerable<Tile> field;

            field = FieldFactory.SimpleField1();
            Assert.AreEqual(1, field.Count());

            field = FieldFactory.SimpleField2();
            Assert.AreEqual(7, field.Count());

            field = FieldFactory.SimpleField3();
            Assert.AreEqual(19, field.Count());

            field = FieldFactory.CustomField3();
            Assert.AreEqual(16, field.Count());
        }

        [Test]
        public void SimpleField2_TileConnectionCount()
        {
            IEnumerable<Tile> field = FieldFactory.SimpleField2();
            int count;

            count = field.First(tile => tile.Position == new Vector3(0, 0, 0)).Connections.Count();
            Assert.AreEqual(6, count);

            IEnumerable<Tile> border = field.Where(tile => tile.Position != new Vector3(0, 0, 0));
            foreach (Tile tile in border)
            {
                count = tile.Connections.Count();
                Assert.AreEqual(3, count);
            }
        }

        [Test]
        public void SimpleField2_TileConnection()
        {
            IEnumerable<Tile> field = FieldFactory.SimpleField2();
            Vector3[] connections;
            Tile tile;

            tile = field.First(t => t.Position == new Vector3(0, 0, 0));
            connections = new Vector3[6]
            {
                new Vector3(1, -1, 0),
                new Vector3(1, 0, -1),
                new Vector3(0, 1, -1),
                new Vector3(-1, 1, 0),
                new Vector3(-1, 0, 1),
                new Vector3(0, -1, 1),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);

            tile = field.First(t => t.Position == new Vector3(1, -1, 0));
            connections = new Vector3[3]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, -1),
                new Vector3(0, -1, 1),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);

            tile = field.First(t => t.Position == new Vector3(1, 0, -1));
            connections = new Vector3[3]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, -1, 0),
                new Vector3(0, 1, -1),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);

            tile = field.First(t => t.Position == new Vector3(0, 1, -1));
            connections = new Vector3[3]
            {
                new Vector3(1, 0, -1),
                new Vector3(-1, 1, 0),
                new Vector3(0, 0, 0),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);

            tile = field.First(t => t.Position == new Vector3(-1, 1, 0));
            connections = new Vector3[3]
            {
                new Vector3(0, 1, -1),
                new Vector3(-1, 0, 1),
                new Vector3(0, 0, 0),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);

            tile = field.First(t => t.Position == new Vector3(-1, 0, 1));
            connections = new Vector3[3]
            {
                new Vector3(-1, 1, 0),
                new Vector3(0, -1, 1),
                new Vector3(0, 0, 0),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);

            tile = field.First(t => t.Position == new Vector3(0, -1, 1));
            connections = new Vector3[3]
            {
                new Vector3(1, -1, 0),
                new Vector3(-1, 0, 1),
                new Vector3(0, 0, 0),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);
        }

        [Test]
        public void CustomField3_TileConnection()
        {
            IEnumerable<Tile> field = FieldFactory.CustomField3();
            Vector3[] connections;
            Tile tile;

            tile = field.First(t => t.Position == new Vector3(0, 0, 0));
            connections = new Vector3[6]
            {
                new Vector3(1, -1, 0),
                new Vector3(1, 0, -1),
                new Vector3(0, 1, -1),
                new Vector3(-1, 1, 0),
                new Vector3(-1, 0, 1),
                new Vector3(0, -1, 1),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);

            tile = field.First(t => t.Position == new Vector3(1, -1, 0));
            connections = new Vector3[3]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, -1),
                new Vector3(0, -1, 1),
            };
            Assert.AreEqual(connections.Count(), tile.Connections.Count());
            Assert.IsTrue(connections.Except(tile.Connections.Select(t => t.Position)).Count() == 0);
        }
    }
}
