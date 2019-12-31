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
    public class DiceTests
    {
        [TestMethod]
        public void RoleDice_ResultsInRandomNumberBetween1and12()
        {
            var dice = new Dice();
            for (int i = 0; i < 10000; i++)
            {
                dice.Roll();
                Int32 roll = dice.RollResults;
                Assert.IsTrue(roll > 0);
                Assert.IsTrue(roll < 13);
            }
        }

        [TestMethod]
        public void RoleDiceMultipleTimes_ShouldDistributeRandomResults()
        {
            Int16 numberOfRolls = 3800;
            Double expectedPercentageOfOccurance = 0.015;

            Int16 numberOfTimesToTry = 100;
            for (int i = 0; i < numberOfTimesToTry; i++)
                RoleDiceAndAssertExpectedPercentage(numberOfRolls, expectedPercentageOfOccurance);
        }

        private void RoleDiceAndAssertExpectedPercentage(Int16 numberOfRolls, Double expectedPercentageOfOccurance)
        {
            Int32[] actualRolls = RollDiceAndReturnResults(numberOfRolls);

            Double expectedMinimumTimesToOccur = numberOfRolls * expectedPercentageOfOccurance;
            for (int i = 2; i < 13; i++)
            {
                Int32 specificRollCount = actualRolls.Count(result => result.Equals(i));
                Assert.IsTrue(specificRollCount >= expectedMinimumTimesToOccur, "Dice Result of {0} did not occur {1} times or more. It occured {2} times.", i.ToString(), expectedMinimumTimesToOccur.ToString(), specificRollCount.ToString());
            }
        }

        private Int32[] RollDiceAndReturnResults(Int16 numberOfRolls)
        {
            Int32[] actualRolls = new Int32[numberOfRolls];
            var dice = new Dice();
            for (int rolls = 0; rolls < numberOfRolls; rolls++)
            {
                dice.Roll();
                actualRolls[rolls] = dice.RollResults;
            }
            return actualRolls;
        }
    }
}
