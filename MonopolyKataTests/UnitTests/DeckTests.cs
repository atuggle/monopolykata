using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyKata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MonopolyKataTests.UnitTests
{
    [TestClass]
    public class DeckTests
    {
        IBanker banker;
        IBoard board;
        IDeck deck;
        IDice dice;
        Queue<ICard> chanceCards;
        Queue<ICard> communityChestCards;

        [TestInitialize]
        public void TestInitialize()
        {
            banker = new Banker();
            dice = new Dice();
            board = new Board(banker, dice);
            deck = new Deck(banker, board);

            chanceCards = new Queue<ICard>(16);
            communityChestCards = new Queue<ICard>(16);
        }

        [TestMethod]
        public void CreateDeck()
        {
            Assert.IsNotNull(deck);
        }

        [TestMethod]
        public void AllChanceCardsExistInDeck()
        {
            var chanceCards = deck.ChanceCards;

            Assert.AreEqual(2, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_ADVANCE_READING_PAY_TWICE_AMOUNT)));

            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_ADVANCE_TO_BOARD_WALK)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_ADVANCE_TO_GO)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_ADVANCE_TO_ILLINOIS)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_ADVANCE_TO_NEAREST_UTILITY)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_ADVANCE_TO_ST_CHARLES)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_BANK_PAYS_50)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_GET_OUT_OF_JAIL)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_GO_BACK_3)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_GO_TO_JAIL)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_LOAN_MATURES_COLLECT_150)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_PAY_EACH_PLAYER_50)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_PAY_TAX_15)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_REPAIRS_25PER_HOUSE_100PER_HOTEL)));
            Assert.AreEqual(1, chanceCards.Count<ICard>(c => c.Name.Equals(Deck.CHANCE_TAKE_RIDE_READING)));
        }

        [TestMethod]
        public void AllCommunityChestCardsExistInDeck()
        {
            var communityChestCards = deck.CommunityChestCards;

            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_ADVANCE_TO_GO)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_BANK_ERROR_COLLECT_200)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_COLLECT_50_EVERY_PLAYER)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_DOCTOR_PAY_50)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_GET_OUT_OF_JAIL)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_GO_TO_JAIL)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_INHERIT_100)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_INSURANCE_MATURES_COLLECT_100)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_PAY_HOSPITAL_100)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_RECIEVE_25)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_REPAIRS_40PER_HOUSE_115PER_HOTEL)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_SCHOOL_TAX_150)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_SELL_STOCK_45)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_TAX_REFUND_20)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_WIN_BEAUTY_CONTEST_10)));
            Assert.AreEqual(1, communityChestCards.Count<ICard>(c => c.Name.Equals(Deck.COMMUNITYCHEST_XMAS_COLLECT_100)));
        }

        [TestMethod]
        public void EnsureRandomizedChanceCards()
        {
            var cards = deck.ChanceCards.ToList<ICard>();
            for (Int32 i = 0; i < 10; i++)
            {
                var newCards = new Deck(banker, board).ChanceCards.ToList<ICard>();
                if (!cards.SequenceEqual(newCards))
                    return;
            }
            Assert.Fail("Randomize order of Chance cards, was not verified");
        }

        [TestMethod]
        public void EnsureRandomizedCommunityChestCards()
        {
            var cards = deck.CommunityChestCards.ToList<ICard>();
            for (Int32 i = 0; i < 10; i++)
            {
                var newCards = new Deck(banker, board).CommunityChestCards.ToList<ICard>();
                if (!cards.SequenceEqual(newCards))
                    return;
            }
            Assert.Fail("Randomize order of Community Chest cards, was not verified");
        }

        [TestMethod]
        public void DrawChanceCardReturnsNextCardReturnsItToBottom()
        {
            var expectedCard = deck.ChanceCards.Peek();
            var drawnCard = deck.DrawChanceCard();

            Assert.AreEqual(expectedCard, drawnCard);
            Assert.AreEqual(expectedCard, deck.ChanceCards.Last());
        }

        [TestMethod]
        public void DrawCommunityChestCardReturnsNextCardReturnsItToBottom()
        {
            var expectedCard = deck.CommunityChestCards.Peek();
            var drawnCard = deck.DrawCommunityChestCard();

            Assert.AreEqual(expectedCard, drawnCard);
            Assert.AreEqual(expectedCard, deck.CommunityChestCards.Last());
        }
    }
}
