using System;

namespace MonopolyKata
{
    public class DiceAwareOwnableProperty : OwnableProperty
    {
        public IDice Dice;

        public DiceAwareOwnableProperty(Int32 boardLocation, IBanker bank, String name, Int32 price, IDice dice)
            : base(boardLocation, bank, name, price, Int32.MinValue) 
        {
            Dice = dice;
        }
    }
}
