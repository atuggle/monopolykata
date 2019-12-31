using System;
using System.Collections.Generic;

namespace MonopolyKata
{
    public class Banker : IBanker
    {
        Dictionary<IPlayer, Int32> accounts = new Dictionary<IPlayer, Int32>();

        public void AddAccount(IPlayer player, Int32 balance)
        {
            accounts.Add(player, balance);
        }

        public Int32 GetPlayerBalance(IPlayer player)
        {
            return accounts[player];
        }

        public void Debit(IPlayer player, Int32 amount)
        {
            accounts[player] -= amount;
        }

        public void Credit(IPlayer player, Int32 amount)
        {
            accounts[player] += amount;
        }

        public void SubmitPayment(IPlayer payer, IPlayer reciever, Int32 amount)
        {
            accounts[payer] -= amount;
            accounts[reciever] += amount;
        }
    }
}
