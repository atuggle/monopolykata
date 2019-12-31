using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Game
    {
        private IBanker banker;
        private IRoundHandler roundHandler;
        private Int32 defaultPlayerBalance = 1500;
        private List<IPlayer> players;

        public Game(IBanker banker, IRoundHandler roundHandler)
        {
            this.banker = banker;
            this.roundHandler = roundHandler;
            players = new List<IPlayer>();
        }

        public IPlayer CreatePlayerAccount(String name)
        {
            if (players.Count() < 9)
            {
                var player = new Player(name);
                players.Add(player);
                banker.AddAccount(player, defaultPlayerBalance);

                return player;
            }

            return null;
        }

        public void RandomizePlayerOrder()
        {
            players = players.OrderBy(p => Guid.NewGuid()).ToList();
        }

        public IPlayer[] GetPlayersInOrder()
        {
            return players.ToArray();
        } 

        public void Play(Int32 rounds)
        {
            var numberOfPlayers = players.Count();
            if (numberOfPlayers > 8 || numberOfPlayers < 2)
                throw new InvalidOperationException();

            roundHandler.PlayRounds(rounds, players.ToArray());
        }
    }
}