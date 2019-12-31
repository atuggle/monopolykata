using System;

namespace MonopolyKata
{
    public class TransactionalProperty : Space
    {
        protected IBanker banker;
        public Int32 Price { get; private set; }
        public Int32? Rent { get; private set; }

        public TransactionalProperty(Int32 boardLocation, IBanker bank, String name, Int32 price, Int32? rent)
            : base(boardLocation, name)
        {
            this.banker = bank;
            this.Price = price;
            this.Rent = rent;
        }
    }

}
