using System;
namespace MonopolyKata
{
    public interface IBanker
    {
        void AddAccount(IPlayer player, Int32 balance);
        Int32 GetPlayerBalance(IPlayer player);
        void Debit(IPlayer player, Int32 amount);
        void Credit(IPlayer player, Int32 amount);
        void SubmitPayment(IPlayer payer, IPlayer reciever, Int32 amount);
    }
}
