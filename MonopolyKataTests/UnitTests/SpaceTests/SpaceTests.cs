using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKataTests.UnitTests
{
    [TestClass]
    public class SpaceTests
    {
        ISpace space;
        IPlayer player;

        [TestInitialize]
        public void TestInitialize()
        {
            player = new Player("test");
            space = new Space(5, "Test");
        }

        [TestMethod]
        public void VisitSpaceUpdatesPlayerLocation()
        {
            space.Visit(player);
            Assert.AreEqual(5, player.Location);
        }

        [TestMethod]
        public void LandOnSpaceUpdatesPlayerLocation()
        {
            space.Land(player);
            Assert.AreEqual(5, player.Location);
        }
    }
}
