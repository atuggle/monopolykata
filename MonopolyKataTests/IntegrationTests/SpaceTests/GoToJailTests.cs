using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class GoToJailTests
    {
        [TestMethod]
        public void PlayerLandsOnGoToJailEndsUpVisitingJail()
        {
            var banker = new Banker();
            var dice = new Dice();
            var board = new Board(banker, dice);
            var goToJail = new GoToJail(30, "Go To Jail", 10);

            var turnHandler = new TurnHandler(board, banker, dice);
            var roundHandler = new RoundHandler(turnHandler);
            var game = new Game(banker, roundHandler);
            var player = game.CreatePlayerAccount("Car");

            goToJail.Land(player);

            Assert.AreEqual(10, player.Location);
        }
    }
}
