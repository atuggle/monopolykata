using System;

namespace MonopolyKata
{
    public class TurnHandler : ITurnHandler
    {
        private IBanker banker;
        private IBoard board;
        private IDice dice;

        public TurnHandler(IBoard board, IBanker banker, IDice dice)
        {
            this.banker = banker;
            this.board = board;
            this.dice = dice;
        }

        public void TakeTurn(IPlayer player, Int32 round, Int32 orderOfPosition)
        {
            TakeTurn(player, round, orderOfPosition, 0);
        }

        private void TakeTurn(IPlayer player, Int32 round, Int32 orderOfPosition, Int32 goAgainCount)
        {
            dice.Roll();

            if (player.InJail)
                SentencedPlayerSpendsTurn(player, round, orderOfPosition, goAgainCount);
            else if (ShouldSendPlayerToJail(goAgainCount))
                SendPlayerToJail(player, round, orderOfPosition, goAgainCount);
            else
                goAgainCount = AdvancePlayerNotInJail(player, round, orderOfPosition, goAgainCount);
        }

        private int AdvancePlayerNotInJail(IPlayer player, Int32 round, Int32 orderOfPosition, Int32 goAgainCount)
        {
            board.AdvancePlayerOnBoard(player, dice.RollResults);

            if (!player.InJail && dice.Doubles)
                TakeTurn(player, round, orderOfPosition, ++goAgainCount);
            else
                TriggerTurnFinishedEvent(player, round, orderOfPosition, goAgainCount);

            return goAgainCount;
        }

        private void SendPlayerToJail(IPlayer player, Int32 round, Int32 orderOfPosition, Int32 goAgainCount)
        {
            board.SendPlayerToJail(player);
            TriggerTurnFinishedEvent(player, round, orderOfPosition, goAgainCount);
        }

        private static bool ShouldSendPlayerToJail(Int32 goAgainCount)
        {
            return goAgainCount > 2;
        }

        private void SentencedPlayerSpendsTurn(IPlayer player, Int32 round, Int32 orderOfPosition, Int32 goAgainCount)
        {
            if (HasSentenceLeft(player))
                SentencedPlayerTakesTurn(player);
            else
                SentencedPlayerPaysToGetOut(player);

            TriggerTurnFinishedEvent(player, round, orderOfPosition, goAgainCount);
        }

        private void SentencedPlayerPaysToGetOut(IPlayer player)
        {
            banker.Debit(player, 50);
            ReleasePlayerFromJail(player);
        }

        private void SentencedPlayerTakesTurn(IPlayer player)
        {
            if (!dice.Doubles)
                --player.SentenceLeft;
            else
                ReleasePlayerFromJail(player);
        }

        private void ReleasePlayerFromJail(IPlayer player)
        {
            player.InJail = false;
            board.AdvancePlayerOnBoard(player, dice.RollResults);
        }

        private static bool HasSentenceLeft(IPlayer player)
        {
            return player.SentenceLeft > 0;
        }

        private void TriggerTurnFinishedEvent(IPlayer player, Int32 round, Int32 orderOfPosition, Int32 goAgainCount)
        {
            var turn = new TurnEventArgs()
            {
                Doubles = dice.Doubles,
                GoAgainCount = goAgainCount,
                OrderOfPosition = orderOfPosition,
                Player = player,
                Roll = dice.RollResults,
                Round = round,
            };

            OnTurnFinished(turn);
        }

        public event EventHandler<TurnEventArgs> TurnFinished;
        protected virtual void OnTurnFinished(TurnEventArgs e)
        {
            EventHandler<TurnEventArgs> handler = TurnFinished;
            if (handler != null)
                handler(this, e);
        }
    }
}
