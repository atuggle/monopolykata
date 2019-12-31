using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyKata;

namespace MonopolyKata
{
    public class TransactionalCard : ICard
    {
        public string Name { get; private set; }

        private IBanker banker;

        public TransactionalCard(IBanker banker, String name)
        {
            Name = name;
            this.banker = banker;
        }

        public void Execute(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
