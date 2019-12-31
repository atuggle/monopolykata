using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;

namespace MonopolyKataTests.UnitTests
{
    [TestClass]
    public class ChanceCardTests
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
