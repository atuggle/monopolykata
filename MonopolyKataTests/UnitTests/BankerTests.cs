using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKataTests.UnitTests
{
    [TestClass]
    public class BankTests
    {
        IBanker banker;
        IPlayer player;
        Int32 playerDefaultBalance = 500;

        [TestInitialize]
        public void TestInitiaize()
        {
            banker = new Banker();
            player = new Player("test");

            banker.AddAccount(player, playerDefaultBalance);
        }

        [TestMethod]
        public void CreateBanker()
        {
            Assert.IsNotNull(banker);
        }

        [TestMethod]
        public void PlayerAccountSuccessfullyCreated()
        {
            var newPlayer = new Player("newPlayer");
            banker.AddAccount(newPlayer, 9999);

            Assert.AreEqual(9999, banker.GetPlayerBalance(newPlayer));
        }

        [TestMethod]
        public void PlayerAccountCreatedWithSpecifiedBalance()
        {
            var newPlayer = new Player("newPlayer");
            banker.AddAccount(newPlayer, -50);

            Assert.AreEqual(-50, banker.GetPlayerBalance(newPlayer));    
        }

        [TestMethod]
        public void CreditAddMoneyToPlayerAccount()
        {
            banker.Credit(player, 50);
            Assert.AreEqual(550, banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void DebitSubtractsMoneyFromPlayerAccount()
        {
            banker.Debit(player, 50);
            Assert.AreEqual(450, banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void GetPlayerBalanceReturnsCorrectBalance()
        {
            banker.Credit(player, 107);
            banker.Debit(player, 7);
            Assert.AreEqual(600, banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void TransferCorrectlyTransfersMoneyFromOnePlayerToAnother()
        {
            var receiver = new Player("receiver");
            banker.AddAccount(receiver, 500);

            banker.SubmitPayment(player, receiver, 55);

            Assert.AreEqual(445, banker.GetPlayerBalance(player));
            Assert.AreEqual(555, banker.GetPlayerBalance(receiver));
        }

        [TestMethod, ExpectedException(typeof(KeyNotFoundException))] 
        public void SubmitPaymentToPlayerWithOutAccountThrowsException()
        {
            var nonAccountPlayer = new Player("nonAccountPlayer");
            banker.SubmitPayment(player, nonAccountPlayer, 50);
        }

        [TestMethod]
        public void AddTwoPlayersWithSameName()
        {
            var player1 = new Player("Car");
            var player2 = new Player("Car");

            banker.AddAccount(player1, 50);
            banker.AddAccount(player2, 50);

            Assert.AreEqual(50, banker.GetPlayerBalance(player1));
            Assert.AreEqual(50, banker.GetPlayerBalance(player2));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))] 
        public void AddPlayerTwiceThrowsException()
        {
            banker.AddAccount(player, 50);
        }
    }
}
