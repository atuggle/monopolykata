using System;

namespace MonopolyKata
{
    public interface IBoard
    {
        void AdvancePlayerOnBoard(IPlayer player, Int32 spaces);
        ISpace GetNextSpace(Int32 currentLocation);
        void SendPlayerToJail(IPlayer player);
    }
}
