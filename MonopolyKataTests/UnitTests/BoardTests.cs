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
    public class BoardTests
    {
        IBoard board;

        [TestInitialize]
        public void TestInitialize()
        {
            var dice = new Dice();
            var banker = new Banker();
            board = new Board(banker, dice);
        }

        [TestMethod]
        public void CreateBoard()
        {
            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void GetNextSpaceReturnsNextSpace()
        {
            var nextSpace = board.GetNextSpace(3);

            Assert.AreEqual("Income Tax", nextSpace.Name);
            Assert.AreEqual(4, nextSpace.BoardLocation);
            Assert.AreEqual(typeof(IncomeTax), nextSpace.GetType());
        }

        [TestMethod]
        public void SendPlayerToJail()
        {
            var player = new Player("jailedPlayer");
            board.SendPlayerToJail(player);

            var jailLocation = 10;
            Assert.AreEqual(jailLocation, player.Location);
            Assert.AreEqual(true, player.InJail);
            Assert.AreEqual(2, player.SentenceLeft);
        }
    }
}
