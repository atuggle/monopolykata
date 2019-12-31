using System;
namespace MonopolyKata
{
    public interface ITurnHandler
    {
        void TakeTurn(IPlayer player, int round, int orderOfPosition);
        event EventHandler<TurnEventArgs> TurnFinished;
    }
}
