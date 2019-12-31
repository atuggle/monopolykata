using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class OwnablePropertyTests
    {
        private IPlayer car;
        private IPlayer horse;
        private IBoard board;
        private IDice dice;
        private IBanker banker = new Banker();

        [TestInitialize]
        public void TestInitialize()
        {
            dice = new Dice();
            board = new Board(banker, dice);

            var turnHandler = new TurnHandler(board, banker, dice);
            var roundHandler = new RoundHandler(turnHandler);
            var game = new Game(banker, roundHandler);

            car = game.CreatePlayerAccount("Car");
            horse = game.CreatePlayerAccount("Horse");
        }

        [TestMethod]
        public void PlayerPurchasesUnownedBalticOnLand()
        {
            var baltic = board.GetNextSpace(2);
            var cashBeforeLand = banker.GetPlayerBalance(car);
            baltic.Land(car);

            Assert.IsTrue(car.OwnsProperty(baltic as IOwnableProperty));
            Assert.AreEqual(60, Math.Abs(cashBeforeLand - banker.GetPlayerBalance(car)));
            Assert.AreEqual(car, ((IOwnableProperty)baltic).Owner);
        }

        [TestMethod]
        public void OwnablePropertyCorrectlyStoresPrice()
        {
            var price = 250;
            var ownable = new OwnableProperty(4, banker, "Test", price, 18);

            Assert.AreEqual(price, ownable.Price);
        }

        [TestMethod]
        public void AllPropertiesInGroupAreOwned_PlayerPaysDoubleRent()
        {
            var baltic = board.GetNextSpace(2);
            var mediterranean = board.GetNextSpace(0);
            var cashBeforeLand = banker.GetPlayerBalance(car);
            baltic.Land(horse);
            mediterranean.Land(horse);

            baltic.Land(car);

            Assert.AreEqual(8, Math.Abs(cashBeforeLand - banker.GetPlayerBalance(car)));
            Assert.AreEqual(horse, ((IOwnableProperty)baltic).Owner);
            Assert.IsFalse(car.OwnsProperty(baltic as IOwnableProperty));
        }
    }
}
