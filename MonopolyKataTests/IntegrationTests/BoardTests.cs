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
    public class BoardTests
    {
        [TestMethod]
        public void PlayerTravelsFiveSpacesFromStartReturnsLocationFive()
        {
            var banker = new Banker();
            var board = new Board(banker, new Dice());
            var player = new Player("Car");

            banker.AddAccount(player, 1500);
            board.AdvancePlayerOnBoard(player, 5);

            Assert.AreEqual(5, player.Location);
        }
    }
}
