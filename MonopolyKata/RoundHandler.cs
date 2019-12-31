using System;

namespace MonopolyKata
{
    public class RoundHandler : IRoundHandler
    {
        private ITurnHandler turnHandler;

        public RoundHandler(ITurnHandler turnHandler)
        {
            this.turnHandler = turnHandler;
        }

        public void PlayRounds(Int32 rounds, IPlayer[] players)
        {
            for (Int32 round = 0; round < rounds; round++)
                PlayCurrentRound(round, players);
        }
        
        private void PlayCurrentRound(Int32 round, IPlayer[] players)
        {
            var turnCount = 0;
            foreach (IPlayer player in players)
                turnHandler.TakeTurn(player, round, turnCount++);
        }
    }
}
