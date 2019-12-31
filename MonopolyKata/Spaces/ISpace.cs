using System;

namespace MonopolyKata
{
    public interface ISpace
    {
        Int32 BoardLocation { get; }
        String Name { get; }

        void Land(IPlayer player);  
        void Visit(IPlayer player);
    }
}
