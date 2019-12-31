using System;

namespace MonopolyKata
{
    public class RailRoadProperty : OwnableProperty
    {
        public RailRoadProperty(Int32 boardLocation, IBanker bank, String name, Int32 price)
            : base(boardLocation, bank, name, price, Int32.MinValue) { }

        public override Int32 CalculatePayment(IPlayer player)
        {
            var rent = 25 * (Int32)Math.Pow(2, NumberOfPropertiesOwned() - 1);
            return rent;
        }
    }
}
