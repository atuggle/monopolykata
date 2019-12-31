using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public class LocationalCard : ICard
    {
        public string Name { get; private set; }

        private IBoard board;

        public LocationalCard(IBoard board, String name)
        {
            Name = name;
            this.board = board;
        }

        public void Execute(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
