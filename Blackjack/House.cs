using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class House
    {
        private static readonly Random rnd = new Random();

        public House(int numDecks)
        {
            Hand = new List<Card>();
            Count = 0;
            Score = 0;
            Deck = CreateDeck(numDecks);
        }

        public IList<Card> Deck { get; set; }
        public IList<Card> Hand { get; set; }
        public int Count { get; set; }
        public int Score { get; set; }
        public bool Holding { get; set; }

        public Card DealCard()
        {
            // Sleep for random seed to pass enough time, don't know a better solution currently
            System.Threading.Thread.Sleep(100);
            int r = rnd.Next(Deck.Count);
            Card Dealt = Deck[r];
            Deck.RemoveAt(r);
            return Dealt;
        }


        public void CalculateCount()
        {
            List<Card> acesLast = new List<Card>();

            Count = 0;
            foreach (var card in Hand)
            {
                if (card.Name != "Ace")
                {
                    Count += card.Value;
                }
                else
                {
                    acesLast.Add(card);
                }
            }

            if (acesLast.Count > 0)
            {
                if (Count + 11 <= 21)
                {
                    Count += 11;
                }
                else
                {
                    Count += 1;
                }
            }

            if (Count >= 21)
            {
                Holding = true;
            }
        }

        public void NewRound()
        {
            Holding = false;
            Hand = new List<Card>();
        }

        public void NewDeck(int numDecks)
        {
            CreateDeck(numDecks);
        }
        private List<Card> CreateDeck(int numDecks)
        {
            var deck = new List<Card>();

            for (int i = 0; i < numDecks; i++)
            {
                deck.Add(new Card { Value = 2, Name = "2", Suit = "Clubs" });
                deck.Add(new Card { Value = 3, Name = "3", Suit = "Clubs" });
                deck.Add(new Card { Value = 4, Name = "4", Suit = "Clubs" });
                deck.Add(new Card { Value = 5, Name = "5", Suit = "Clubs" });
                deck.Add(new Card { Value = 6, Name = "6", Suit = "Clubs" });
                deck.Add(new Card { Value = 7, Name = "7", Suit = "Clubs" });
                deck.Add(new Card { Value = 8, Name = "8", Suit = "Clubs" });
                deck.Add(new Card { Value = 9, Name = "9", Suit = "Clubs" });
                deck.Add(new Card { Value = 10, Name = "10", Suit = "Clubs" });
                deck.Add(new Card { Value = 10, Name = "Jack", Suit = "Clubs" });
                deck.Add(new Card { Value = 10, Name = "Queen", Suit = "Clubs" });
                deck.Add(new Card { Value = 10, Name = "King", Suit = "Clubs" });
                deck.Add(new Card { Value = 11, Name = "Ace", Suit = "Clubs" });
                deck.Add(new Card { Value = 2, Name = "2", Suit = "Hearts" });
                deck.Add(new Card { Value = 3, Name = "3", Suit = "Hearts" });
                deck.Add(new Card { Value = 4, Name = "4", Suit = "Hearts" });
                deck.Add(new Card { Value = 5, Name = "5", Suit = "Hearts" });
                deck.Add(new Card { Value = 6, Name = "6", Suit = "Hearts" });
                deck.Add(new Card { Value = 7, Name = "7", Suit = "Hearts" });
                deck.Add(new Card { Value = 8, Name = "8", Suit = "Hearts" });
                deck.Add(new Card { Value = 9, Name = "9", Suit = "Hearts" });
                deck.Add(new Card { Value = 10, Name = "10", Suit = "Hearts" });
                deck.Add(new Card { Value = 10, Name = "Jack", Suit = "Hearts" });
                deck.Add(new Card { Value = 10, Name = "Queen", Suit = "Hearts" });
                deck.Add(new Card { Value = 10, Name = "King", Suit = "Hearts" });
                deck.Add(new Card { Value = 11, Name = "Ace", Suit = "Hearts" });
                deck.Add(new Card { Value = 2, Name = "2", Suit = "Spades" });
                deck.Add(new Card { Value = 3, Name = "3", Suit = "Spades" });
                deck.Add(new Card { Value = 4, Name = "4", Suit = "Spades" });
                deck.Add(new Card { Value = 5, Name = "5", Suit = "Spades" });
                deck.Add(new Card { Value = 6, Name = "6", Suit = "Spades" });
                deck.Add(new Card { Value = 7, Name = "7", Suit = "Spades" });
                deck.Add(new Card { Value = 8, Name = "8", Suit = "Spades" });
                deck.Add(new Card { Value = 9, Name = "9", Suit = "Spades" });
                deck.Add(new Card { Value = 10, Name = "10", Suit = "Spades" });
                deck.Add(new Card { Value = 10, Name = "Jack", Suit = "Spades" });
                deck.Add(new Card { Value = 10, Name = "Queen", Suit = "Spades" });
                deck.Add(new Card { Value = 10, Name = "King", Suit = "Spades" });
                deck.Add(new Card { Value = 11, Name = "Ace", Suit = "Spades" });
                deck.Add(new Card { Value = 2, Name = "2", Suit = "Diamonds" });
                deck.Add(new Card { Value = 3, Name = "3", Suit = "Diamonds" });
                deck.Add(new Card { Value = 4, Name = "4", Suit = "Diamonds" });
                deck.Add(new Card { Value = 5, Name = "5", Suit = "Diamonds" });
                deck.Add(new Card { Value = 6, Name = "6", Suit = "Diamonds" });
                deck.Add(new Card { Value = 7, Name = "7", Suit = "Diamonds" });
                deck.Add(new Card { Value = 8, Name = "8", Suit = "Diamonds" });
                deck.Add(new Card { Value = 9, Name = "9", Suit = "Diamonds" });
                deck.Add(new Card { Value = 10, Name = "10", Suit = "Diamonds" });
                deck.Add(new Card { Value = 10, Name = "Jack", Suit = "Diamonds" });
                deck.Add(new Card { Value = 10, Name = "Queen", Suit = "Diamonds" });
                deck.Add(new Card { Value = 10, Name = "King", Suit = "Diamonds" });
                deck.Add(new Card { Value = 11, Name = "Ace", Suit = "Diamonds" });
            }

            return deck;
        }
    }
}
