using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class PlayerTests
    {
        private IDice dice;
        private Game game;
        private IBoard board;
        private IPlayer playerCar;
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

            playerCar = game.CreatePlayerAccount("Car");
        }

        [TestMethod]
        public void FirstPlayerMovesSeven_ResultOfMoveIsLocation7()
        {
            board.AdvancePlayerOnBoard(playerCar, 7);

            Int16 newLocation = 7;
            Assert.AreEqual(newLocation, playerCar.Location);
        }

        [TestMethod]
        public void PlayerAtLocation39Moves6_ResultsAtLocation5()
        {
            var adjustedPlayer = game.CreatePlayerAccount("Test");
            adjustedPlayer.Location = 39;

            board.AdvancePlayerOnBoard(adjustedPlayer, 6);

            Int16 newLocation = 5;
            Assert.AreEqual(newLocation, adjustedPlayer.Location);
        }

        [TestMethod]
        public void PlayerTakesTurn_ResultsInPlayerRollingDiceAndMoving()
        {
            var startLocation = playerCar.Location;
            board.AdvancePlayerOnBoard(playerCar, 11);
            Assert.AreNotEqual(startLocation, playerCar.Location);
        }

        [TestMethod]
        public void PlayerLandsOnGoToJail_EndsUpVisitingJailWithBalanceUnchanged()
        {
            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);

            board.AdvancePlayerOnBoard(playerCar, 30);

            Assert.AreEqual(10, playerCar.Location);
            Assert.AreEqual(0, banker.GetPlayerBalance(playerCar) - cashBeforeTurn);
        }

        [TestMethod]
        public void PlayerPassesOverGoToJail_BalanceIsUnchangedAndLocation()
        {
            var cashBeforeGo = banker.GetPlayerBalance(playerCar);
            var goToJail = new GoToJail(30, "Go To Jail", 10);
            playerCar.Location = 29;

            goToJail.Visit(playerCar);

            Assert.AreEqual(30, playerCar.Location);
            Assert.AreEqual(0, banker.GetPlayerBalance(playerCar) - cashBeforeGo);
        }

        [TestMethod]
        public void InitializedPlayerHasDefault1500Balance()
        {
            var player = game.CreatePlayerAccount("Test");

            Assert.AreEqual(1500, banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void PlayerLandsOnUnOwnedBoardwalkAndBuysIt()
        {
            var boardwalk = board.GetNextSpace(38) as IOwnableProperty;
            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);

            board.AdvancePlayerOnBoard(playerCar, 39);

            Assert.AreEqual(boardwalk.Price, cashBeforeTurn - banker.GetPlayerBalance(playerCar));
            Assert.AreEqual(boardwalk.Owner, playerCar);
            Assert.IsTrue(playerCar.OwnsProperty(boardwalk));
        }

        [TestMethod]
        public void PlayerLandsOnPropertyPlayerOwnsNothingHappens()
        {
            var mediterranean = board.GetNextSpace(0);
            mediterranean.Land(playerCar);

            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);
            mediterranean.Land(playerCar);

            Assert.AreEqual(0, cashBeforeTurn - banker.GetPlayerBalance(playerCar));
        }

        [TestMethod]
        public void AddPropertyAddsPassedInPropertyToPlayer()
        {
            var testProperty = new OwnableProperty(10, banker, "Test Property", 999, 999);
            playerCar.AddProperty(testProperty);

            Assert.IsTrue(playerCar.OwnsProperty(testProperty));
        }

        [TestMethod]
        public void PlayerPassesOverUnownedPropertyDoesNotPurchase()
        {
            var testProperty = new OwnableProperty(10, banker, "Test Property", 999, 999);
            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);

            testProperty.Visit(playerCar);

            Assert.IsFalse(playerCar.OwnsProperty(testProperty));
            Assert.AreEqual(0, cashBeforeTurn - banker.GetPlayerBalance(playerCar));
        }
    }
}
