using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKataTests.IntegrationTests
{
    [TestClass]
    public class BankerTests
    {
        IPlayer car;
        IPlayer horse;
        IBanker bank;
        IBoard board;
        IBanker banker = new Banker();
        Game game;

        [TestInitialize]
        public void TestInitiaize()
        {
            board = new Board(banker, new Dice());
            bank = new Banker();

            var turnHandler = new TurnHandler(board, banker, new Dice());
            var roundHandler = new RoundHandler(turnHandler);
            game = new Game(banker, roundHandler);
            car = game.CreatePlayerAccount("Car");
            horse = game.CreatePlayerAccount("Horse");
        }

        [TestMethod]
        public void GetPlayerBalanceFromBankShouldEqual1500()
        {
            bank.AddAccount(car, 1500);

            var playerBalance = bank.GetPlayerBalance(car);

            Assert.AreEqual(1500, playerBalance);
        }

        [TestMethod]
        public void AddMultiplePlayersGetSpecifiedPlayerBalance()
        {
            bank.AddAccount(car, 999);
            bank.AddAccount(game.CreatePlayerAccount("Horse"), 800);
            bank.AddAccount(game.CreatePlayerAccount("Chair"), 900);
            bank.AddAccount(game.CreatePlayerAccount("Stool"), 700);
            bank.AddAccount(game.CreatePlayerAccount("Test"), 600);
            bank.AddAccount(game.CreatePlayerAccount("ONE"), 500);

            Assert.AreEqual(999, bank.GetPlayerBalance(car));
        }

        [TestMethod]
        public void CarPaysHors150InRent()
        {
            bank.AddAccount(car, 1500);
            bank.AddAccount(horse, 1500);

            bank.SubmitPayment(car, horse, 150);

            Assert.AreEqual(1350, bank.GetPlayerBalance(car));
            Assert.AreEqual(1650, bank.GetPlayerBalance(horse));
        }
    }
}
