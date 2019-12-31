using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyKata
{
    public class Deck : IDeck
    {
        public const String CHANCE_BANK_PAYS_50 = "Bank pays you $50";
        public const String CHANCE_PAY_EACH_PLAYER_50 = "Elected Chairman, pay each player $50";
        public const String CHANCE_PAY_TAX_15 = "Pay poor tax $15";
        public const String CHANCE_LOAN_MATURES_COLLECT_150 = "Building load matures, collect $150";
        public const String CHANCE_REPAIRS_25PER_HOUSE_100PER_HOTEL = "General Repairs, $25 each house, $100 each hotel";
        public const String CHANCE_ADVANCE_TO_GO = "Advance to Go";
        public const String CHANCE_GO_TO_JAIL = "Go to Jail";
        public const String CHANCE_ADVANCE_READING_PAY_TWICE_AMOUNT = "Advance to nearest RailRoad, pay owner twice the rental";
        public const String CHANCE_ADVANCE_TO_ILLINOIS = "Advance to Illinois Ave";
        public const String CHANCE_GO_BACK_3 = "Go back 3 spaces";
        public const String CHANCE_ADVANCE_TO_BOARD_WALK = "Advance to Board Walk";
        public const String CHANCE_GET_OUT_OF_JAIL = "Get out of Jail Free";
        public const String CHANCE_ADVANCE_TO_NEAREST_UTILITY = "Advance to nearest Utility, throw dice pay 10 times dice amount";
        public const String CHANCE_ADVANCE_TO_ST_CHARLES = "Advance to St. Charles Place";
        public const String CHANCE_TAKE_RIDE_READING = "Take a ride on the Reading Rail Road";

        public const String COMMUNITYCHEST_XMAS_COLLECT_100 = "XMas fund matures collect $100";
        public const String COMMUNITYCHEST_INHERIT_100 = "Inherit $100";
        public const String COMMUNITYCHEST_SELL_STOCK_45 = "Sell stock $45";
        public const String COMMUNITYCHEST_BANK_ERROR_COLLECT_200 = "Bank error collect $200";
        public const String COMMUNITYCHEST_PAY_HOSPITAL_100 = "Pay hospital $100";
        public const String COMMUNITYCHEST_DOCTOR_PAY_50 = "Doctor's fee pay $50";
        public const String COMMUNITYCHEST_RECIEVE_25 = "Recieve $25 for Services";
        public const String COMMUNITYCHEST_SCHOOL_TAX_150 = "Pay school tax $150";
        public const String COMMUNITYCHEST_WIN_BEAUTY_CONTEST_10 = "Beauty contest collect $10";
        public const String COMMUNITYCHEST_COLLECT_50_EVERY_PLAYER = "Collect $50 from every player";
        public const String COMMUNITYCHEST_TAX_REFUND_20 = "Income tax refund $20";
        public const String COMMUNITYCHEST_REPAIRS_40PER_HOUSE_115PER_HOTEL = "Repairs $40 per house, $115 per hotel";
        public const String COMMUNITYCHEST_INSURANCE_MATURES_COLLECT_100 = "Life Insurance matures collect $100";
        public const String COMMUNITYCHEST_GET_OUT_OF_JAIL = "Get out of Jail Free";
        public const String COMMUNITYCHEST_ADVANCE_TO_GO = "Advance to Go";
        public const String COMMUNITYCHEST_GO_TO_JAIL = "Go to Jail";

        public Queue<ICard> ChanceCards { get; private set; }
        public Queue<ICard> CommunityChestCards { get; private set; }
        private IBanker banker;
        private IBoard board;

        public Deck(IBanker banker, IBoard board)
        {
            this.banker = banker;
            this.board = board;

            ChanceCards = new Queue<ICard>(16);
            CommunityChestCards = new Queue<ICard>(16);

            InitializeCards();
        }

        private void InitializeCards()
        {
            GenerateRandomChanceCards();
            GenerateRandomCommunityChestCards();
        }

        private void GenerateRandomChanceCards()
        {
            List<ICard> chanceCards = new List<ICard>(16);
            chanceCards.Add(new TransactionalCard(banker, CHANCE_BANK_PAYS_50));
            chanceCards.Add(new TransactionalCard(banker, CHANCE_PAY_EACH_PLAYER_50));
            chanceCards.Add(new TransactionalCard(banker, CHANCE_PAY_TAX_15));
            chanceCards.Add(new TransactionalCard(banker, CHANCE_LOAN_MATURES_COLLECT_150));
            chanceCards.Add(new TransactionalCard(banker, CHANCE_REPAIRS_25PER_HOUSE_100PER_HOTEL));
            chanceCards.Add(new LocationalCard(board, CHANCE_ADVANCE_TO_GO));
            chanceCards.Add(new LocationalCard(board, CHANCE_GO_TO_JAIL));
            chanceCards.Add(new LocationalCard(board, CHANCE_ADVANCE_READING_PAY_TWICE_AMOUNT));
            chanceCards.Add(new LocationalCard(board, CHANCE_ADVANCE_READING_PAY_TWICE_AMOUNT));
            chanceCards.Add(new LocationalCard(board, CHANCE_ADVANCE_TO_ILLINOIS));
            chanceCards.Add(new LocationalCard(board, CHANCE_GO_BACK_3));
            chanceCards.Add(new LocationalCard(board, CHANCE_ADVANCE_TO_BOARD_WALK));
            chanceCards.Add(new LocationalCard(board, CHANCE_GET_OUT_OF_JAIL));
            chanceCards.Add(new LocationalCard(board, CHANCE_ADVANCE_TO_NEAREST_UTILITY));
            chanceCards.Add(new LocationalCard(board, CHANCE_ADVANCE_TO_ST_CHARLES));
            chanceCards.Add(new LocationalCard(board, CHANCE_TAKE_RIDE_READING));

            chanceCards = chanceCards.OrderBy(p => Guid.NewGuid()).ToList();
            foreach (var c in chanceCards)
                ChanceCards.Enqueue(c);
        }

        private void GenerateRandomCommunityChestCards()
        {
            List<ICard> communityChestCards = new List<ICard>(16);
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_XMAS_COLLECT_100));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_INHERIT_100));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_SELL_STOCK_45));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_BANK_ERROR_COLLECT_200));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_PAY_HOSPITAL_100));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_DOCTOR_PAY_50));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_RECIEVE_25));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_SCHOOL_TAX_150));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_WIN_BEAUTY_CONTEST_10));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_COLLECT_50_EVERY_PLAYER));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_TAX_REFUND_20));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_REPAIRS_40PER_HOUSE_115PER_HOTEL));
            communityChestCards.Add(new TransactionalCard(banker, COMMUNITYCHEST_INSURANCE_MATURES_COLLECT_100));
            communityChestCards.Add(new LocationalCard(board, COMMUNITYCHEST_GET_OUT_OF_JAIL));
            communityChestCards.Add(new LocationalCard(board, COMMUNITYCHEST_ADVANCE_TO_GO));
            communityChestCards.Add(new LocationalCard(board, COMMUNITYCHEST_GO_TO_JAIL));

            communityChestCards = communityChestCards.OrderBy(p => Guid.NewGuid()).ToList();
            foreach (var c in communityChestCards)
                CommunityChestCards.Enqueue(c);
        }

        public ICard DrawChanceCard()
        {
            var drawnCard = ChanceCards.Dequeue();
            ChanceCards.Enqueue(drawnCard);

            return drawnCard;
        }

        public ICard DrawCommunityChestCard()
        {
            var drawnCard = CommunityChestCards.Dequeue();
            CommunityChestCards.Enqueue(drawnCard);

            return drawnCard;
        }
    }
}
