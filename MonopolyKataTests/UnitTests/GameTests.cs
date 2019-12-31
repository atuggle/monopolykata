using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKataTests.UnitTests
{
    [TestClass]
    public class GameTests
    {
        IBanker banker;
        IBoard board;
        IDice dice;
        Game game;
        IPlayer[] players;
        Mock<IRoundHandler> mockRoundHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            banker = new Banker();
            dice = new Dice();
            board = new Board(banker, dice);

            var turnHandler = new TurnHandler(board, banker, dice);
            mockRoundHandler = new Mock<IRoundHandler>();

            game = new Game(banker, mockRoundHandler.Object);

            players = new Player[2];
            players[0] = game.CreatePlayerAccount("One");
            players[1] = game.CreatePlayerAccount("Two");
        }

        [TestMethod]
        public void CreatGame()
        {
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void PlayGame()
        {
            game.Play(5);

            var players = game.GetPlayersInOrder();
            mockRoundHandler.Verify(r => r.PlayRounds(5, players), Times.Exactly(1));
        }

        [TestMethod]
        public void GetPlayersInOrder()
        {
            Assert.IsTrue(players.SequenceEqual(game.GetPlayersInOrder()));
        }

        [TestMethod]
        public void RandomizePlayerOrder()
        {
            for (Int32 i = 0; i < 10; i++)
            {
                game.RandomizePlayerOrder();
                if (!players.SequenceEqual(game.GetPlayersInOrder()))
                    return;
            }
            Assert.Fail("RandomizePlayerOrder, never changed order of players");
        }

        [TestMethod]
        public void CreatePlayerAccountCreatesPlayer()
        {
            var player = game.CreatePlayerAccount("testPlayer");

            Assert.AreEqual(player, game.GetPlayersInOrder().FirstOrDefault(p => p.ToString().Equals("testPlayer")));
        }
    }
}
