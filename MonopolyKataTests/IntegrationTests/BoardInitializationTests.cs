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
    public class BoardInitializationTests
    {
        private IBoard board;
        private IBanker banker = new Banker();

        [TestInitialize]
        public void TestInitialize()
        {
            board = new Board(banker, new Dice());
        }

        [TestMethod]
        public void PropertyInGroupIsLinkedToOtherPropertiesInGroup()
        {
            var groupedSpaces = new List<IOwnableProperty>();
            var mediterranian = board.GetNextSpace(0) as IOwnableProperty;
            var baltic = board.GetNextSpace(2) as IOwnableProperty;
            groupedSpaces.Add(mediterranian as IOwnableProperty);
            groupedSpaces.Add(baltic as IOwnableProperty);

            Assert.IsTrue(groupedSpaces.SequenceEqual(mediterranian.GroupedSpaces));
            Assert.IsTrue(groupedSpaces.SequenceEqual(baltic.GroupedSpaces));
        }

        [TestMethod]
        public void BoardwalkAndParkplaceAreInSameGroup()
        {
            var groupedSpaces = new List<IOwnableProperty>();
            var boardwalk = board.GetNextSpace(36) as IOwnableProperty;
            var parkplace = board.GetNextSpace(38) as IOwnableProperty;
            groupedSpaces.Add(boardwalk);
            groupedSpaces.Add(parkplace);

            Assert.IsTrue(groupedSpaces.SequenceEqual(boardwalk.GroupedSpaces));
            Assert.IsTrue(groupedSpaces.SequenceEqual(parkplace.GroupedSpaces));
        }

        [TestMethod]
        public void RailRoadsAreInSameGroup()
        {

            var groupedSpaces = new List<IOwnableProperty>();
            var reading = board.GetNextSpace(4) as IOwnableProperty;
            var penn = board.GetNextSpace(14) as IOwnableProperty;
            var bAndO = board.GetNextSpace(24) as IOwnableProperty;
            var shortLine = board.GetNextSpace(34) as IOwnableProperty;
            groupedSpaces.Add(reading);
            groupedSpaces.Add(penn);
            groupedSpaces.Add(bAndO);
            groupedSpaces.Add(shortLine);

            Assert.IsTrue(groupedSpaces.SequenceEqual(reading.GroupedSpaces));
            Assert.IsTrue(groupedSpaces.SequenceEqual(penn.GroupedSpaces));
            Assert.IsTrue(groupedSpaces.SequenceEqual(bAndO.GroupedSpaces));
            Assert.IsTrue(groupedSpaces.SequenceEqual(shortLine.GroupedSpaces));
        }

        [TestMethod]
        public void UtilitiesAreInSameGroup()
        {
            var groupedSpaces = new List<IOwnableProperty>();
            var electric = board.GetNextSpace(11) as IOwnableProperty;
            var water = board.GetNextSpace(27) as IOwnableProperty;
            groupedSpaces.Add(electric);
            groupedSpaces.Add(water);

            Assert.IsTrue(groupedSpaces.SequenceEqual(electric.GroupedSpaces));
            Assert.IsTrue(groupedSpaces.SequenceEqual(water.GroupedSpaces));
        }
    }
}
