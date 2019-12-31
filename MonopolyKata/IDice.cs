using System;

namespace MonopolyKata
{
    public interface IDice
    {
        Int32 RollResults { get; }
        Boolean Doubles { get; }

        void Roll();
    }
}
