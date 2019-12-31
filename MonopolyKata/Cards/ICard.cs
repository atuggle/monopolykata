using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public interface ICard
    {
        String Name { get; }
        void Execute(IPlayer player);
    }
}
