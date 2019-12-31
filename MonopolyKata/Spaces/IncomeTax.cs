using System;

namespace MonopolyKata
{
    public class IncomeTax : TransactionalProperty
    {
        public IncomeTax(Int32 boardLocation, IBanker bank, String name) : base(boardLocation, bank, name, 0, null) { }

        public override void Land(IPlayer player)
        {
            base.Land(player);

            var amountToSubtract = Math.Min(Convert.ToInt32(banker.GetPlayerBalance(player) * 0.10), 200);
            banker.Debit(player, amountToSubtract);
        }
    }
}