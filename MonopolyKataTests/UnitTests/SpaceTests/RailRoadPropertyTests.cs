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
    public class RailRoadPropertyTests
    {
        IBanker banker;
        IBoard board;
        IDice dice;
        IPlayer player;
        IPlayer owner;
        RailRoadProperty railRoad1;
        RailRoadProperty railRoad2;
        RailRoadProperty railRoad3;
        RailRoadProperty railRoad4;

        [TestInitialize]
        public void TestInitialize()
        {
            dice = new Dice();
            banker = new Banker();
            board = new Board(banker, dice);
            player = new Player("test");
            owner = new Player("Owner");
            banker.AddAccount(owner, 1500);

            railRoad1 = new RailRoadProperty(5, banker, "railRoad1", 200);
            railRoad2 = new RailRoadProperty(15, banker, "railRoad2", 200);
            railRoad3 = new RailRoadProperty(25, banker, "railRoad3", 200);
            railRoad4 = new RailRoadProperty(35, banker, "railRoad4", 200);
            var groupedSpaces = new List<IOwnableProperty>() { railRoad1, railRoad2, railRoad3, railRoad4 };
            railRoad1.GroupedSpaces = groupedSpaces;
            railRoad2.GroupedSpaces = groupedSpaces;
            railRoad3.GroupedSpaces = groupedSpaces;
            railRoad4.GroupedSpaces = groupedSpaces;

            dice.Roll();
        }

        [TestMethod]
        public void CreateRailRoad()
        {
            Assert.IsNotNull(railRoad1);
            Assert.IsNotNull(railRoad2);
            Assert.IsNotNull(railRoad3);
            Assert.IsNotNull(railRoad4); 
        }

        [TestMethod]
        public void CalculatePaymentOneOwned()
        {
            var expectedPayment = 25;
            railRoad1.Land(owner);

            var actualPayment = railRoad1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);
        }

        [TestMethod]
        public void CalculatePaymentTwoOwned()
        {
            var expectedPayment = 50;
            railRoad1.Land(owner);
            railRoad2.Land(owner);

            var actualPayment = railRoad1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);
        }

        [TestMethod]
        public void CalculatePayementThreeOwned()
        {
            var expectedPayment = 100;
            railRoad1.Land(owner);
            railRoad2.Land(owner);
            railRoad3.Land(owner);

            var actualPayment = railRoad1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);

        }

        [TestMethod]
        public void CalculatePayementFourOwned()
        {
            var expectedPayment = 200;
            railRoad1.Land(owner);
            railRoad2.Land(owner);
            railRoad3.Land(owner);
            railRoad4.Land(owner);

            var actualPayment = railRoad1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);
        }
    }
}
