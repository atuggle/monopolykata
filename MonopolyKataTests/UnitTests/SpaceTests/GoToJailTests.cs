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
    public class GoToJailTests
    {
        Int32 jailLocation = 10;
        GoToJail goToJail;
        IPlayer player;

        [TestInitialize]
        public void TestInitiaize()
        {
            goToJail = new GoToJail(30, "goToJail", jailLocation);
            player = new Player("player");
        }

        [TestMethod]
        public void LandPutsPlayerInJail()
        {
            goToJail.Land(player);

            Assert.IsTrue(player.InJail);
            Assert.AreEqual(jailLocation, player.Location);
            Assert.AreEqual(2, player.SentenceLeft);
        }

        [TestMethod]
        public void LandDoesNotAlterPlayerBalance()
        {
            var banker = new Banker();
            var expectedBalance = 500;
            banker.AddAccount(player, expectedBalance);

            goToJail.Land(player);

            Assert.AreEqual(expectedBalance, banker.GetPlayerBalance(player));
        }
    }
}
