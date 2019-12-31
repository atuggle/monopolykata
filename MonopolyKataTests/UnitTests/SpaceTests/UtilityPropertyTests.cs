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
    public class UtilityPropertyTests
    {
        IBanker banker;
        IBoard board;
        IDice dice;
        IPlayer player;
        IPlayer owner;
        UtilityProperty utility1;
        UtilityProperty utility2;

        [TestInitialize]
        public void TestInitialize()
        {
            dice = new Dice();
            banker = new Banker();
            board = new Board(banker, dice);
            player = new Player("test");
            owner = new Player("Owner");
            banker.AddAccount(owner, 500);

            utility1 = new UtilityProperty(12, banker, "utility1", 150, dice);
            utility2 = new UtilityProperty(28, banker, "utility2", 150, dice);
            var groupedSpaces = new List<IOwnableProperty>() { utility1, utility2 };
            utility1.GroupedSpaces = groupedSpaces;
            utility2.GroupedSpaces = groupedSpaces;

            dice.Roll();
        }

        [TestMethod]
        public void CreateUtility()
        {
            Assert.IsNotNull(utility1);
            Assert.IsNotNull(utility2);
        }

        [TestMethod]
        public void CalculatePaymentBothOwned()
        {
            var expectedPayment = dice.RollResults * 10;
            utility1.Land(owner);
            utility2.Land(owner);

            var actualPayment = utility1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);
        }

        [TestMethod]
        public void CalculatePaymentOneOwned()
        {
            utility1.Land(owner);

            var expectedPayment = dice.RollResults * 4;
            var actualPayment = utility1.CalculatePayment(player);

            Assert.AreEqual(expectedPayment, actualPayment);   
        }
    }
}
