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
    public class IncomeTaxTests
    {
        IBanker banker;
        IPlayer player;
        IncomeTax incomeTax;

        [TestInitialize]
        public void TestInitialize()
        {
            player = new Player("player");
            banker = new Banker();
            incomeTax = new IncomeTax(2, banker, "IncomeTax");

            banker.AddAccount(player, 500);
        }

        [TestMethod]
        public void LandUpdatesPlayerLocation()
        {
            incomeTax.Land(player);
            Assert.AreEqual(incomeTax.BoardLocation, player.Location);
        }

        [TestMethod]
        public void LandDebitsPlayerAccount200()
        {
            banker.Credit(player, 2000);
            var expectedTax = 200;
            var beforeLandCash = banker.GetPlayerBalance(player);

            incomeTax.Land(player);

            Assert.AreEqual(expectedTax, beforeLandCash - banker.GetPlayerBalance(player));
        }

        [TestMethod]
        public void LandDebitsPlayerAccount10Percent()
        {
            var beforeLandCash = banker.GetPlayerBalance(player);
            var expectedTax = Convert.ToInt32(beforeLandCash * 0.10);
            incomeTax.Land(player);

            Assert.AreEqual(expectedTax, beforeLandCash - banker.GetPlayerBalance(player));
        }
    }
}
