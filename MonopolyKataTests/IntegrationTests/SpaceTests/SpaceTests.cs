using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class SpaceTests
    {
        private IBoard board;
        private IPlayer playerCar;
        private IBanker banker = new Banker();

        [TestInitialize]
        public void TestInitialize()
        {
            var dice = new Dice();
            board = new Board(banker, dice);
            var turnHandler = new TurnHandler(board, banker, dice);
            var roundHandler = new RoundHandler(turnHandler);
            var game = new Game(banker, roundHandler);

            playerCar = game.CreatePlayerAccount("Car");
        }

        [TestMethod]
        public void VisitPropertyUpdatesPlayerLocation()
        {
            for (Int32 i = 0; i < 40; i++)
            {
                var space = board.GetNextSpace(i);
                space.Visit(playerCar);

                Assert.AreEqual(space.BoardLocation, playerCar.Location);
            }
        }

        [TestMethod]
        public void LandOnPropertyUpdatesPlayerLocation()
        {
            var someProperty = new Space(20, "Test");
            someProperty.Land(playerCar);

            Assert.AreEqual(someProperty.BoardLocation, playerCar.Location);
        }

        [TestMethod]
        public void LandOnGoToJailPutsPlayerInJail()
        {
            var someProperty = new GoToJail(30, "Go To Jail", 10);
            someProperty.Land(playerCar);

            Assert.AreEqual(10, playerCar.Location);
        }
    }
}
