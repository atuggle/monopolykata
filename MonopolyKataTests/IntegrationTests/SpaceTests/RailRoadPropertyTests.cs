using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class RailRoadPropertyTests
    {
        private IBoard board;
        private RailRoadProperty readingRailRoad;
        private RailRoadProperty pennsylvaniaRailRoad;
        private RailRoadProperty shortLineRailRoad;
        private RailRoadProperty bAndORailRoad;
        private IPlayer player;
        private IPlayer horse;
        private Game game;
        private IBanker banker = new Banker();

        [TestInitialize]
        public void TestInitialize()
        {
            var dice = new Dice();
            board = new Board(banker, dice);
            readingRailRoad = board.GetNextSpace(4) as RailRoadProperty;
            pennsylvaniaRailRoad = board.GetNextSpace(14) as RailRoadProperty;
            bAndORailRoad = board.GetNextSpace(24) as RailRoadProperty;
            shortLineRailRoad = board.GetNextSpace(34) as RailRoadProperty;

            var turnHandler = new TurnHandler(board, banker, dice);
            var roundHandler = new RoundHandler(turnHandler);
            game = new Game(banker, roundHandler);

            player = game.CreatePlayerAccount("Car");
            horse = game.CreatePlayerAccount("Horse");
        }

        [TestMethod]
        public void PlayerLandsOnOwnedRailRoadPaysRent()
        {
            pennsylvaniaRailRoad.Land(game.CreatePlayerAccount("Horse"));
            var cashBeforeVisit = banker.GetPlayerBalance(player);
            pennsylvaniaRailRoad.Land(player);

            Assert.AreEqual(25, cashBeforeVisit - banker.GetPlayerBalance(player));
            Assert.IsFalse(player.OwnsProperty(pennsylvaniaRailRoad));
        }

        [TestMethod]
        public void PlayerLandsOnUnownedRailRoad_PurchasesIt()
        {
            var cashBeforeVisit = banker.GetPlayerBalance(player);
            shortLineRailRoad.Land(player);

            Assert.AreEqual(((IOwnableProperty)shortLineRailRoad).Price, cashBeforeVisit - banker.GetPlayerBalance(player));
            Assert.IsTrue(player.OwnsProperty(shortLineRailRoad));
        }

        [TestMethod]
        public void PlayerVisitsRailRoad_NothingHappens()
        {
            var cashBeforeVisit = banker.GetPlayerBalance(player);
            readingRailRoad.Visit(player);

            Assert.AreEqual(0, banker.GetPlayerBalance(player) - cashBeforeVisit);
        }

        [TestMethod]
        public void FourRailRoadsOwned_PlayerPays200Dollars()
        {
            readingRailRoad.Land(horse);
            pennsylvaniaRailRoad.Land(horse);
            bAndORailRoad.Land(horse);
            shortLineRailRoad.Land(horse);
            var cashBeforeLand = banker.GetPlayerBalance(player);

            readingRailRoad.Land(player);

            Assert.AreEqual(200, cashBeforeLand - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void ThreeRailRoadsOwned_PlayerPays100Dollars()
        {
            readingRailRoad.Land(horse);
            pennsylvaniaRailRoad.Land(horse);
            bAndORailRoad.Land(horse);
            var cashBeforeLand = banker.GetPlayerBalance(player);

            readingRailRoad.Land(player);

            Assert.AreEqual(100, cashBeforeLand - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void TwoRailRoadsOwned_PlayerPays50Dollars()
        {
            readingRailRoad.Land(horse);
            pennsylvaniaRailRoad.Land(horse);
            var cashBeforeLand = banker.GetPlayerBalance(player);

            readingRailRoad.Land(player);

            Assert.AreEqual(50, cashBeforeLand - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void SingleRailRoadsOwned_PlayerPays25Dollars()
        {
            readingRailRoad.Land(horse);
            var cashBeforeLand = banker.GetPlayerBalance(player);

            readingRailRoad.Land(player);

            Assert.AreEqual(25, cashBeforeLand - banker.GetPlayerBalance(player));
        }
    }
}
