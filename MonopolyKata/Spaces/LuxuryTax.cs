using System;

namespace MonopolyKata
{
    public class LuxuryTax : TransactionalProperty
    {
        public LuxuryTax(Int32 boardLocation, IBanker bank, String name) : base(boardLocation, bank, name, 0, 75) { }

        public override void Land(IPlayer player)
        {
            base.Land(player);
            banker.Debit(player, Rent.GetValueOrDefault(0));
        }
    }
}
