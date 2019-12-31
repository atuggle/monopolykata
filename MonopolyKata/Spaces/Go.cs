using System;

namespace MonopolyKata
{
    public class Go : TransactionalProperty
    {
        public Go(Int32 boardLocation, IBanker bank, String name) : base(boardLocation, bank, name, 0, null) { }

        public override void Visit(IPlayer player)
        {
            base.Visit(player);
            banker.Credit(player, 200);
        }
    }
}
