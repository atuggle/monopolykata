using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class GameTests
    {
        private Game game;
        private IBoard board;
        private IDice dice;
        private IBanker banker = new Banker();
        private IPlayer playerCar;
        private IPlayer playerHorse;
        private TurnHandler turnHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            dice = new Dice();
            board = new Board(banker, dice);

            turnHandler = new TurnHandler(board, banker, dice);
            var roundHandler = new RoundHandler(turnHandler);
            game = new Game(banker, roundHandler);

            playerCar = game.CreatePlayerAccount("Car");
            playerHorse = game.CreatePlayerAccount("Horse");
        }

        [TestMethod]
        public void CreateGameWithTwoPlayers_ResultsInGameWithTwoPlayers()
        {
            var players = new List<Player>();

            game.Play(1);

            var orderedPlayers = game.GetPlayersInOrder();
            Assert.IsTrue(orderedPlayers.Count() == 2);
            Assert.IsTrue(orderedPlayers.Contains(playerCar));
            Assert.IsTrue(orderedPlayers.Contains(playerHorse));
        }

        [TestMethod]
        public void PlayGame_DoesNotThrowException()
        {
            var round = 1;
            game.Play(round);
        }

        [TestMethod, ExpectedExceptionAttribute(typeof(InvalidOperationException))]
        public void PlayGameWithMoreThan8Players_ExceptionShouldBeThrown()
        {
            game.CreatePlayerAccount("Three");
            game.CreatePlayerAccount("Four");
            game.CreatePlayerAccount("Five");
            game.CreatePlayerAccount("Six");
            game.CreatePlayerAccount("Seven");
            game.CreatePlayerAccount("Eight");
            game.CreatePlayerAccount("Nine");

            var round = 1;
            game.Play(round);
        }

        [TestMethod, ExpectedExceptionAttribute(typeof(InvalidOperationException))]
        public void PlayGameWithLessThan2Players_ExceptionShouldBeThrown()
        {
            var board = new Board(banker, dice);

            var turnHandler = new TurnHandler(board, banker, dice);
            var roundHandler = new RoundHandler(turnHandler);
            Game testGame = new Game(banker, roundHandler);

            testGame.CreatePlayerAccount("Car");

            var round = 1;
            testGame.Play(round);
        }

        [TestMethod]
        public void Create100GamesWithTwoPlayers_ValidateRandomOrderOfTwoPlayers()
        {
            game.Play(1);
            var expectedPlayers = game.GetPlayersInOrder();
            for (int i = 0; i < 100; i++)
            {
                var actualPlayers = GeneratePlayersForNewGame();
                if (PlayerOrderHasChanged(expectedPlayers, actualPlayers))
                    return;
            }

            Assert.Fail("Played 100 games with Horse and Car and they were always in the same order");
        }

        private IPlayer[] GeneratePlayersForNewGame()
        {
            var round = 1;
            game.RandomizePlayerOrder();
            game.Play(round);
            return game.GetPlayersInOrder();
        }

        private bool PlayerOrderHasChanged(IPlayer[] expectedPlayers, IPlayer[] actualPlayers)
        {
            return !expectedPlayers.SequenceEqual(actualPlayers);
        }

        [TestMethod]
        public void PlayGameWith20Rounds_EachPlayerTook20Turns()
        {
            var roundsToPlay = 20;
            game.RandomizePlayerOrder();
            var players = game.GetPlayersInOrder();

            Int32 carTotalRounds = 0;
            Int32 horseTotalRounds = 0;
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(playerCar)) carTotalRounds++; };
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(playerHorse)) horseTotalRounds++; };

            game.Play(roundsToPlay);

            Assert.AreEqual(20, carTotalRounds, "Total rounds PlayerCar took was not equal to expected 20");
            Assert.AreEqual(20, horseTotalRounds, "Total rounds PlayerHorse took was not equal to expected 20");
        }

        [TestMethod]
        public void PlayGameWith20Rounds_VerifyPlayersPlayedEachRoundInOrder()
        {
            var roundsToPlay = 20;
            game.RandomizePlayerOrder();

            var players = game.GetPlayersInOrder();

            List<TurnEventArgs> firstPlayerTurns = new List<TurnEventArgs>();
            List<TurnEventArgs> secondPlayerTurns = new List<TurnEventArgs>();
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(players[0])) firstPlayerTurns.Add(e); };
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(players[1])) secondPlayerTurns.Add(e); };

            game.Play(roundsToPlay);

            for (int round = 0; round < roundsToPlay; round++)
                AssertPlayerOrder(firstPlayerTurns, secondPlayerTurns, round);
        }

        private static void AssertPlayerOrder(List<TurnEventArgs> firstPlayerTurns, List<TurnEventArgs> secondPlayerTurns, int round)
        {
            var firstPlayerPosition = firstPlayerTurns[round].OrderOfPosition;
            var secondPlayerPosition = secondPlayerTurns[round].OrderOfPosition;
            if (firstPlayerPosition >= secondPlayerPosition)
                Assert.Fail(string.Format("First player did not go first every time {0}:{1}", firstPlayerTurns[round].OrderOfPosition, secondPlayerTurns[round].OrderOfPosition));
        }

        [TestMethod]
        public void PlayerContinuoslyRollsDoubles_EndsUpInJail()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.RollResults).Returns(4);
            mockDice.Setup(d => d.Doubles).Returns(true);

            var board = new Board(banker, mockDice.Object);
            var turnHandler = new TurnHandler(board, banker, mockDice.Object);
            var roundHandler = new RoundHandler(turnHandler);
            var testGame = new Game(banker, roundHandler);

            var playerCar = testGame.CreatePlayerAccount("Car");
            var playerHorse = testGame.CreatePlayerAccount("Horse");
            var playerIron = testGame.CreatePlayerAccount("Iron");

            List<TurnEventArgs> playerTurns = new List<TurnEventArgs>();
            turnHandler = new TurnHandler(board, banker, mockDice.Object);
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(playerCar)) playerTurns.Add(e); };

            turnHandler.TakeTurn(playerCar, 0, 0);

            var jailLocation = 10;
            Assert.AreEqual(3, playerTurns[0].GoAgainCount);
            Assert.AreEqual(jailLocation, playerCar.Location);
            Assert.IsTrue(playerCar.InJail);
        }

        [TestMethod]
        public void PlayerSpendsThreeTurnsInJailPays50ToGetOut()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.RollResults).Returns(3);
            mockDice.Setup(d => d.Doubles).Returns(false);

            var board = new Board(banker, mockDice.Object);
            var turnHandler = new TurnHandler(board, banker, mockDice.Object);
            var roundHandler = new RoundHandler(turnHandler);
            var testGame = new Game(banker, roundHandler);


            var playerCar = testGame.CreatePlayerAccount("Car");

            board.SendPlayerToJail(playerCar);

            turnHandler = new TurnHandler(board, banker, mockDice.Object);
            List<TurnEventArgs> playerTurns = new List<TurnEventArgs>();
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(playerCar)) playerTurns.Add(e); };

            var jailLocation = 10;

            turnHandler.TakeTurn(playerCar, 0, 0);
            Assert.AreEqual(jailLocation, playerCar.Location);
            Assert.IsTrue(playerCar.InJail);
            Assert.AreEqual(1500, banker.GetPlayerBalance(playerCar));

            turnHandler.TakeTurn(playerCar, 1, 0);
            Assert.AreEqual(jailLocation, playerCar.Location);
            Assert.IsTrue(playerCar.InJail);
            Assert.AreEqual(1500, banker.GetPlayerBalance(playerCar));

            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);
            var costSpace13 = 140;
            turnHandler.TakeTurn(playerCar, 2, 0);
            Assert.AreEqual(13, playerCar.Location);
            Assert.IsFalse(playerCar.InJail);
            Assert.AreEqual(costSpace13 + 50, cashBeforeTurn - banker.GetPlayerBalance(playerCar));
        }

        [TestMethod]
        public void PlayerGoesToJailDoesNotCollect200ForPassingGo()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.RollResults).Returns(3);
            mockDice.Setup(d => d.Doubles).Returns(false);

            var board = new Board(banker, mockDice.Object);
            var turnHandler = new TurnHandler(board, banker, mockDice.Object);
            var roundHandler = new RoundHandler(turnHandler);
            var testGame = new Game(banker, roundHandler);

            var playerCar = testGame.CreatePlayerAccount("Car");

            playerCar.Location = 27;
            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);
            board.AdvancePlayerOnBoard(playerCar, 3);

            var jailLocation = 10;
            Assert.AreEqual(jailLocation, playerCar.Location);
            Assert.IsTrue(playerCar.InJail);
            Assert.AreEqual(0, cashBeforeTurn - banker.GetPlayerBalance(playerCar));
        }

        [TestMethod]
        public void PlayerInJailRollsDoublesToGetOut()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.RollResults).Returns(4);
            mockDice.Setup(d => d.Doubles).Returns(true);

            var board = new Board(banker, mockDice.Object);
            var turnHandler = new TurnHandler(board, banker, mockDice.Object);
            var roundHandler = new RoundHandler(turnHandler);
            var testGame = new Game(banker, roundHandler);

            var playerCar = testGame.CreatePlayerAccount("Car");

            board.SendPlayerToJail(playerCar);

            List<TurnEventArgs> playerTurns = new List<TurnEventArgs>();
            turnHandler = new TurnHandler(board, banker, mockDice.Object);
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(playerCar)) playerTurns.Add(e); };

            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);
            turnHandler.TakeTurn(playerCar, 0, 0);

            Assert.AreEqual(14, playerCar.Location);
            Assert.IsFalse(playerCar.InJail);
            Assert.AreEqual(0 + 160, cashBeforeTurn - banker.GetPlayerBalance(playerCar));
        }

        [TestMethod]
        public void PlayerRollsDoublesLandsOnGoToJailDoesNotGoAgain()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.RollResults).Returns(4);
            mockDice.Setup(d => d.Doubles).Returns(true);

            var board = new Board(banker, mockDice.Object);
            var turnHandler = new TurnHandler(board, banker, mockDice.Object);
            var roundHandler = new RoundHandler(turnHandler);
            var testGame = new Game(banker, roundHandler);

            var playerCar = testGame.CreatePlayerAccount("Car");

            playerCar.Location = 26;
            var cashBeforeTurn = banker.GetPlayerBalance(playerCar);

            turnHandler = new TurnHandler(board, banker, mockDice.Object);
            List<TurnEventArgs> playerTurns = new List<TurnEventArgs>();
            turnHandler.TurnFinished += (s, e) => { if (e.Player.Equals(playerCar)) playerTurns.Add(e); };

            turnHandler.TakeTurn(playerCar, 0, 0);

            var jailLocation = 10;
            Assert.AreEqual(0, playerTurns[0].GoAgainCount);
            Assert.AreEqual(jailLocation, playerCar.Location);
            Assert.IsTrue(playerCar.InJail);
            Assert.AreEqual(0, cashBeforeTurn - banker.GetPlayerBalance(playerCar));
        }
    }
}
