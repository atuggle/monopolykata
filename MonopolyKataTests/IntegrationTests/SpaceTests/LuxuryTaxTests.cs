using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class LuxuryTaxTests
    {
        private IPlayer player;
        private Mock<IDice> mockDice;
        private Game game;
        private IBoard board;
        private IBanker banker = new Banker();

        [TestInitialize]
        public void TestInitialize()
        {
            mockDice = new Mock<IDice>();
            board = new Board(banker, mockDice.Object);

            var turnHandler = new TurnHandler(board, banker, mockDice.Object);
            var roundHandler = new RoundHandler(turnHandler);
            game = new Game(banker, roundHandler);

            player = game.CreatePlayerAccount("Test");
        }

        [TestMethod]
        public void PlayerLandsOnLuxuryTax_Loses75Dollars()
        {
            banker.Credit(player, 175);
            var cashBeforeTurn = banker.GetPlayerBalance(player);

            board.AdvancePlayerOnBoard(player, 38);

            Assert.AreEqual(38, player.Location);
            Assert.AreEqual(75, cashBeforeTurn - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void PlayerVisitsLuxuryTax_NothingHappens()
        {
            var luxuryTax = new LuxuryTax(38, banker, "Luxury Tax");
            var cashBeforeTax = banker.GetPlayerBalance(player);

            luxuryTax.Visit(player);

            Assert.AreEqual(0, banker.GetPlayerBalance(player) - cashBeforeTax);
            Assert.AreEqual(38, player.Location);
        }
    }
}
