using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class GoTests
    {
        private Mock<IDice> mockDice;
        private Game game;
        private IBoard board;
        private ISpace go;
        private IPlayer playerCar;
        private IBanker banker = new Banker();

        [TestInitialize]
        public void TestInitialize()
        {
            mockDice = new Mock<IDice>();
            board = new Board(banker, mockDice.Object);
            go = board.GetNextSpace(39);

            var turnHandler = new TurnHandler(board, banker, mockDice.Object);
            var roundHandler = new RoundHandler(turnHandler);
            game = new Game(banker, roundHandler);

            playerCar = game.CreatePlayerAccount("Car");
        }

        [TestMethod]
        public void PlayerLandsOnGo_Collects200Dollars()
        {
            var cashBeforeLandOnGo = banker.GetPlayerBalance(playerCar);

            go.Land(playerCar);

            Assert.AreEqual(200, banker.GetPlayerBalance(playerCar)  - cashBeforeLandOnGo);
        }

        [TestMethod]
        public void PlayerStartsOnGoLandsOnSpace1_DoesNotCollect200Dollars()
        {
            var cashBeforeGo = banker.GetPlayerBalance(playerCar);
            board.AdvancePlayerOnBoard(playerCar, 1);

            Assert.AreEqual(1, playerCar.Location);
            Assert.AreEqual(-60, banker.GetPlayerBalance(playerCar) - cashBeforeGo);
        }

        [TestMethod]
        public void PlayerMovesPassGo_Collects200Dollars()
        {
            var cashBeforeGo = banker.GetPlayerBalance(playerCar);
            var go = new Go(0, banker, "Go");

            go.Visit(playerCar);

            Assert.AreEqual(200, banker.GetPlayerBalance(playerCar) - cashBeforeGo);
        }

        [TestMethod]
        public void PlayerLandsOnGoTwiceResultsIn400ExtraDollars()
        {
            var cashBeforeGo = banker.GetPlayerBalance(playerCar);
            var go = new Go(0, banker, "Go");

            go.Land(playerCar);
            go.Land(playerCar);

            Assert.AreEqual(400, banker.GetPlayerBalance(playerCar) - cashBeforeGo);
        }
    }
}
