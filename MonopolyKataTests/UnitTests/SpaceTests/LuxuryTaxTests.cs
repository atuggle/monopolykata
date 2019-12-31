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
    public class LuxuryTaxTests
    {
        IBanker banker;
        IPlayer player;
        LuxuryTax luxuryTax;

        [TestInitialize]
        public void TestInitialize()
        {
            player = new Player("player");
            banker = new Banker();
            luxuryTax = new LuxuryTax(2, banker, "LuxuryTax");

            banker.AddAccount(player, 500);
        }

        [TestMethod]
        public void LandUpdatesPlayerLocation()
        {
            luxuryTax.Land(player);
            Assert.AreEqual(luxuryTax.BoardLocation, player.Location);
        }

        [TestMethod]
        public void LandDebitsPlayerAccount()
        {
            var beforeLandCash = banker.GetPlayerBalance(player);
            luxuryTax.Land(player);

            Assert.AreEqual(luxuryTax.Rent, beforeLandCash - banker.GetPlayerBalance(player));
        }
    }
}
