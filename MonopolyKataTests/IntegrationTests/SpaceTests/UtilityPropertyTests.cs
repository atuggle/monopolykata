using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;
using System;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class UtilityPropertyTests
    {
        private IBoard board;
        private IDice dice;
        private IPlayer player;
        private IPlayer horse;
        private UtilityProperty electric;
        private UtilityProperty water;
        private Game game;
        private IBanker banker;

        [TestInitialize]
        public void TestInitialize()
        {
            dice = new Dice();
            banker = new Banker();
            board = new Board(banker, dice);

            var turnHandler = new TurnHandler(board, banker, dice);
            var roundHandler = new RoundHandler(turnHandler);
            game = new Game(banker, roundHandler);

            electric = board.GetNextSpace(11) as UtilityProperty;
            water = board.GetNextSpace(27) as UtilityProperty;

            player = game.CreatePlayerAccount("Car");
            horse = game.CreatePlayerAccount("Horse");
        }

        [TestMethod]
        public void PlayerLandsOnUnownedUtility_PurchasesIt()
        {
            var cashBeforeVisit = banker.GetPlayerBalance(player);
            water.Land(player);

            Assert.AreEqual(((IOwnableProperty)water).Price, cashBeforeVisit - banker.GetPlayerBalance(player));
            Assert.IsTrue(player.OwnsProperty(water as IOwnableProperty));
        }

        [TestMethod]
        public void PlayerVisitsUtility_NothingHappens()
        {
            var cashBeforeVisit = banker.GetPlayerBalance(player);
            water.Visit(player);

            Assert.AreEqual(0, banker.GetPlayerBalance(player) - cashBeforeVisit);
        }
    }
}
