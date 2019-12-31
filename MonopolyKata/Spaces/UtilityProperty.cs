using System;

namespace MonopolyKata
{
    public class UtilityProperty : DiceAwareOwnableProperty
    {
        public UtilityProperty(Int32 boardLocation, IBanker bank, String name, Int32 price, IDice dice)
            : base(boardLocation, bank, name, price, dice) { }

        public override Int32 CalculatePayment(IPlayer player)
        {
            if (NumberOfPropertiesOwned() < 2)
                return 4 * Dice.RollResults;

            return 10 * Dice.RollResults;
        }
    }
}
