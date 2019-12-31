using System;

namespace MonopolyKata
{
    public class Dice : IDice
    {
        public Int32 RollResults { get; private set; }
        public Boolean Doubles { get; private set; }

        private static Random random = new Random();

        public void Roll()
        {
            var firstDie = random.Next(1, 7);
            var secondDie = random.Next(1, 7);

            RollResults = firstDie + secondDie;
            Doubles = firstDie.Equals(secondDie) ? true : false;
        }
    }
}
