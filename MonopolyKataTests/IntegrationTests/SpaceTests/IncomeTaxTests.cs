using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyKata;
using Moq;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class IncomeTaxTests
    {
        private IPlayer player;
        private IDice dice;
        private Game game;
        private IBoard board;
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
            player = game.CreatePlayerAccount("Test");
        }

        [TestMethod]
        public void PlayerWith1800LandsOnIncomeTax_Lose180Dollars()
        {
            banker.Credit(player, 300);
            var cashBeforeTurn = banker.GetPlayerBalance(player);

            board.AdvancePlayerOnBoard(player, 4);

            Assert.AreEqual(4, player.Location);
            Assert.AreEqual(180, cashBeforeTurn - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void PlayerWith2200LandsOnIncomeTax_Lose200Dollars()
        {
            banker.Credit(player, 2200);
            var cashBeforeTurn = banker.GetPlayerBalance(player);

            board.AdvancePlayerOnBoard(player, 4);

            Assert.AreEqual(4, player.Location);
            Assert.AreEqual(200, cashBeforeTurn - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void PlayerWithZeroBalanceLandsOnIncomeTax_BalanceIsLeftAtZero()
        {
            banker.Debit(player, 1500);
            var cashBeforeTurn = banker.GetPlayerBalance(player);

            board.AdvancePlayerOnBoard(player, 4);

            Assert.AreEqual(4, player.Location);
            Assert.AreEqual(0, cashBeforeTurn - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void PlayerWith2000LandsOnIncomeTax_Lose200Dollars()
        {
            banker.Credit(player, 2000);
            var cashBeforeTurn = banker.GetPlayerBalance(player);

            board.AdvancePlayerOnBoard(player, 4);
            Assert.AreEqual(4, player.Location);
            Assert.AreEqual(200, cashBeforeTurn - banker.GetPlayerBalance(player));
        }
    }
}
