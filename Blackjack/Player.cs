using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public int Score { get; set; }
        public int Count { get; set; }
        public bool Holding { get; set; }

        public Player()
        {
            Hand = new List<Card>();
            Score = 0;
            Count = 0;
        }

        public void CalculateCount()
        {
            List<Card> acesLast = new List<Card>();

            Count = 0;
            foreach (var card in Hand)
            {
                if(card.Name != "Ace")
                {
                    Count += card.Value;
                }
                else
                {
                    acesLast.Add(card);
                }
            }

            if(acesLast.Count > 0)
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

            if(Count >= 21)
            {
                Holding = true;
            }
        }

        public void NewRound()
        {
            Holding = false;
            Hand = new List<Card>();
        }
    }
}
