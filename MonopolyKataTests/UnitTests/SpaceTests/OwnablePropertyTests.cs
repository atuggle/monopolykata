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
    public class OwnablePropertyTests
    {
        IBanker banker;
        IBoard board;
        IDice dice;
        IPlayer player;
        IPlayer owner;
        OwnableProperty ownable1;
        OwnableProperty ownable2;
        OwnableProperty ownable3;
        OwnableProperty ownable4;

        [TestInitialize]
        public void TestInitialize()
        {
            dice = new Dice();
            banker = new Banker();
            board = new Board(banker, dice);
            player = new Player("player");
            owner = new Player("Owner");
            banker.AddAccount(player, 1500);
            banker.AddAccount(owner, 1500);

            ownable1 = new OwnableProperty(5, banker, "ownable1", 50, 25);
            ownable2 = new OwnableProperty(15, banker, "ownable2", 50, 25);
            ownable3 = new OwnableProperty(25, banker, "ownable3", 50, 25);
            ownable4 = new OwnableProperty(35, banker, "ownable4", 50, 25);
            var groupedSpaces = new List<IOwnableProperty>() { ownable1, ownable2, ownable3, ownable4 };
            ownable1.GroupedSpaces = groupedSpaces;
            ownable2.GroupedSpaces = groupedSpaces;
            ownable3.GroupedSpaces = groupedSpaces;
            ownable4.GroupedSpaces = groupedSpaces;
        }

        [TestMethod]
        public void VisitUpdatesPlayerLocation()
        {
            ownable1.Visit(player);
            Assert.AreEqual(ownable1.BoardLocation, player.Location);
        }

        [TestMethod]
        public void LandUpdatesPlayerLocation()
        {
            ownable1.Land(player);
            Assert.AreEqual(ownable1.BoardLocation, player.Location);
        }

        [TestMethod]
        public void LandPurchasesUnOwnedProperty()
        {
            Assert.IsNull(ownable1.Owner);

            ownable1.Land(player);
            
            Assert.AreEqual(ownable1.BoardLocation, player.Location);
            Assert.AreEqual(player, ownable1.Owner);
        }

        [TestMethod]
        public void CalculatePayementAllOwned()
        {
            var expectedPayment = ownable1.Rent*2;
            ownable1.Land(owner);
            ownable2.Land(owner);
            ownable3.Land(owner);
            ownable4.Land(owner);

            var actualPayment = ownable1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);
        }

        [TestMethod]
        public void CalculatePayementNotAllOwned()
        {
            var expectedPayment = ownable1.Rent;
            ownable1.Land(owner);
            ownable2.Land(owner);

            var actualPayment = ownable1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);
        }
    }
}
