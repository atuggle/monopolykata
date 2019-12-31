using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;

namespace MonopolyKataTests.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        IBanker banker;
        IBoard board;
        IDice dice;
        IPlayer player;

        [TestInitialize]
        public void TestInitialize()
        {
            player = new Player("test");

            dice = new Dice();
            banker = new Banker();
            board = new Board(banker, dice);
        }

        [TestMethod]
        public void CreatePlayer()
        {
            Assert.IsNotNull(player);
            Assert.AreEqual("test", player.ToString());
        }

        [TestMethod]
        public void AddPropertyResultsInAddedProperty()
        {
            var railRoad = new RailRoadProperty(5, banker, "RailRoad", 50);
            player.AddProperty(railRoad);

            Assert.IsTrue(player.OwnsProperty(railRoad));
        }

        [TestMethod]
        public void OwnsPropertyCorrectlyIdentifiesIfPropertyIsOwned()
        {
            var playerOwns = new RailRoadProperty(5, banker, "playerOwns", 50);
            var playerDoesNotOwn = new UtilityProperty(12, banker, "playerDoesNotOwn", 75, dice);
            player.AddProperty(playerOwns);

            Assert.IsTrue(player.OwnsProperty(playerOwns));
            Assert.IsFalse(player.OwnsProperty(playerDoesNotOwn));
        }
    }
}
