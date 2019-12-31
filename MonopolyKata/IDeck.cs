using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public interface IDeck
    {
        Queue<ICard> ChanceCards { get; }
        Queue<ICard> CommunityChestCards { get; }
        ICard DrawChanceCard();
        ICard DrawCommunityChestCard();
    }
}
