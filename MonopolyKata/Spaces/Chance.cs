using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public class Chance : TransactionalProperty
    {
        private IBoard baord;
        private IDeck deck;

        public Chance(IBoard board, Int32 boardLocation, IBanker bank, IDeck deck, String name)
            : base(boardLocation, bank, name, 0, null) 
        {
            this.baord = board;
            this.deck = deck;
        }

        public override void Land(IPlayer player)
        {
            base.Land(player);

            //var board.GetNextChanceCard
        }
    }
}
