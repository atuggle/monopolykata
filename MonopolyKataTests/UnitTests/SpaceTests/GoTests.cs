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
    public class GoTests
    {
        Go go;
        IBanker banker;
        IPlayer player;

        [TestInitialize]
        public void TestInitialize()
        {
            banker = new Banker();
            go = new Go(0, banker, "Go");
            player = new Player("player");

            banker.AddAccount(player, 1500);
        }

        [TestMethod]
        public void VisitUpdatesPlayerLocation()
        {
            var expectedLocation = 0;
            player.Location = 35;
            go.Visit(player);

            Assert.AreEqual(expectedLocation, player.Location);
        }

        [TestMethod]
        public void VisitCreditsPlayerBalance200Dollars()
        {
            var expectedIncrease = 200;
            var cashBeforeLand = banker.GetPlayerBalance(player);
            go.Visit(player);

            Assert.AreEqual(expectedIncrease, banker.GetPlayerBalance(player) - cashBeforeLand);
        }

        [TestMethod]
        public void LandUpdatesPlayerLocation()
        {
            var expectedLocation = 0;
            player.Location = 35;
            go.Land(player);

            Assert.AreEqual(expectedLocation, player.Location);
        }

        [TestMethod]
        public void LandCreditsPlayerBalance200Dollars()
        {
            var expectedIncrease = 200;
            var cashBeforeLand = banker.GetPlayerBalance(player);
            go.Land(player);

            Assert.AreEqual(expectedIncrease, banker.GetPlayerBalance(player) - cashBeforeLand);
        }
    }
}
