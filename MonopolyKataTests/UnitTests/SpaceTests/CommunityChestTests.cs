using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;

namespace MonopolyKataTests.UnitTests.SpaceTests
{
    [TestClass]
    public class CommunityChestTests
    {
        Mock<IBoard> board = new Mock<IBoard>();
        Mock<IBanker> banker = new Mock<IBanker>();

        [TestMethod]
        public void DrawChanceCardInokesExecute()
        {

            //var chanceCard = new Chance(board, 0, banker, deck, "test");
        }
    }
}
