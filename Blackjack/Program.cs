using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        public static string NumPlayers;
        public static int NumDecks;
        public static List<Player> Players;
        public static House House;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack!");
            
            Console.WriteLine("Please enter the number of players ");
            NumPlayers = Console.ReadLine();

            int num = 0;
            while (num <= 0)
            {
                if (Int32.TryParse(NumPlayers, out num))
                {
                    Players = new List<Player>();
                    for (int i = 0; i < num; i++)
                    {
                        Players.Add(new Player { Name = "Player " + (i + 1) });
                    }

                    NumPlayers = num.ToString();
                }
                else
                {
                    Console.WriteLine("Please insert a positive number.");
                }
            }

            Console.WriteLine("Please enter the number of decks to be played with.");
            NumDecks = 0;
            while (NumDecks <= 0)
            {
                string numDecks = Console.ReadLine();
                if (Int32.TryParse(numDecks, out NumDecks))
                {
                    House = new House(NumDecks);
                }
                else
                {
                    Console.WriteLine("Please insert a positive number.");
                }
            }

            bool playing = true;
            while (playing)
            {
                // Deal out players initial 2 cards.
                foreach (var player in Players)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        player.Hand.Add(House.DealCard());
                    }
                }

                // Deal out House initial 2 card.
                for (int i = 0; i < 2; i++)
                {
                    House.Hand.Add(House.DealCard());
                }

                // Begin game
                bool dealing = true;
                while (dealing)
                {
                    DrawTable(true);

                    // Players turn
                    foreach (var player in Players)
                    {
                        if (player.Count < 21 && !player.Holding)
                        {
                            Console.WriteLine(player.Name + " Hit(t) or Hold(h)?");
                            var choice = Console.ReadLine();
                            if (choice == "t")
                            {
                                player.Hand.Add(House.DealCard());
                                player.CalculateCount();
                            }
                            else if (choice == "h")
                            {
                                player.CalculateCount();
                                player.Holding = true;
                            }
                        }
                    }

                    House.CalculateCount();
                    // House turn (May want more logic with Ace cards...)
                    // House logic could use some more work
                    if (House.Hand.Contains(new Card { Name = "Ace" }))
                    {
                        if (House.Count < 18 && House.Hand.Count < 3)
                        {
                            House.Hand.Add(House.DealCard());
                        }
                    }
                    else if (House.Count < 17)
                    {
                        House.Hand.Add(House.DealCard());
                    }
                    else if (Players.FindAll(FindHigherScore).Count > 0)
                    {
                        House.Hand.Add(House.DealCard());
                    }
                    else if(Players.FindAll(FindHolders).Count == Players.Count)
                    {
                        House.Holding = true;
                    }

                    if(House.Holding)
                    {
                        dealing = false;
                    }

                }

                // Calculate Scores
                // Scoring needs more work
                foreach (var player in Players)
                {
                    if (player.Count > House.Count && player.Count <= 21) // Player beats house
                    {
                        player.Score += 1;
                    }else if(House.Count > 21 && player.Count <= 21) // House busts and player didn't bust
                    {
                        player.Score += 1;
                    }
                    else // House gets the point
                    {
                        House.Score += 1;
                    }

                    // Not exactly sure how to score if multiple players and house reach 21 in 2 cards.
                    if (player.Count == 21 && player.Hand.Count == 2 && House.Count != 21 && House.Hand.Count != 2)
                    {
                        player.Score += 2;
                    }
                }
                
                // Display final table and scores
                DrawTable(false);
                DrawScores();
                Console.WriteLine("Play again? Yes(y) or No(n)?");
                var playAgain = Console.ReadLine();
                if(playAgain == "n")
                {
                    playing = false;
                }
                else
                {
                    House.NewDeck(NumDecks);
                    House.NewRound();
                    foreach (var player in Players)
                    {
                        player.NewRound();
                    }
                }
            }
        }

        private static bool FindHolders(Player player)
        {
            if(player.Holding == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool FindHigherScore(Player player)
        {
            if(player.Count > House.Count && player.Count < 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void DrawTable(bool drawHidden)
        {
            Console.Clear();
            
            // Draw House
            Console.WriteLine("House");
            
            if (drawHidden) // Draw the first card hidden
            {
                for (int i = 0; i < House.Hand.Count; i++)
                {
                    Console.Write("  _______ ");
                }
                Console.WriteLine();
                Console.Write(" |      | ");
                for (int i = 1; i < House.Hand.Count; i++)
                {
                    if (House.Hand[i].Name == "Ace" || House.Hand[i].Name == "King" || House.Hand[i].Name == "Queen" || House.Hand[i].Name == "Jack")
                    {
                        Console.Write(" | " + House.Hand[i].Name.Substring(0, 1) + "  " + House.Hand[i].Suit.Substring(0, 1) + " | ");
                    }
                    else
                    {
                        if (House.Hand[i].Value.ToString().Length > 1)
                        {
                            Console.Write(" | " + House.Hand[i].Value + " " + House.Hand[i].Suit.Substring(0, 1) + " | ");
                        }
                        else
                        {
                            Console.Write(" | " + House.Hand[i].Value + "  " + House.Hand[i].Suit.Substring(0, 1) + " | ");
                        }
                    }
                }
                
                Console.WriteLine();

                for (int i = 0; i < House.Hand.Count; i++)
                {
                    Console.Write(" |______| ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < House.Hand.Count; i++)
                {
                    Console.Write("  _______ ");
                }
                Console.WriteLine();
                foreach (var card in House.Hand)
                {
                    if (card.Name == "Ace" || card.Name == "King" || card.Name == "Jack" || card.Name == "Queen")
                    {
                        Console.Write(" | " + card.Name.Substring(0, 1) + "  " + card.Suit.Substring(0, 1) + " | ");
                    }
                    else
                    {
                        if (card.Value.ToString().Length > 1)
                        {
                            Console.Write(" | " + card.Value + " " + card.Suit.Substring(0, 1) + " | ");
                        }
                        else
                        {
                            Console.Write(" | " + card.Value + "  " + card.Suit.Substring(0, 1) + " | ");
                        }

                    }
                }
                Console.WriteLine();

                for (int i = 0; i < House.Hand.Count; i++)
                {
                    Console.Write(" |______| ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
            
            // Draw player hands
            foreach (var player in Players)
            {
                Console.WriteLine(player.Name);
                for (int i = 0; i < player.Hand.Count; i++)
                {
                    Console.Write("  _______ ");
                }
                Console.WriteLine();
                foreach (var card in player.Hand)
                {
                    if (card.Name == "Ace" || card.Name == "King" || card.Name == "Jack" || card.Name == "Queen")
                    {
                        Console.Write(" | " + card.Name.Substring(0, 1) + "  " + card.Suit.Substring(0, 1) + " | ");
                    }
                    else
                    {
                        if (card.Value.ToString().Length > 1)
                        {
                            Console.Write(" | " + card.Value + " " + card.Suit.Substring(0, 1) + " | ");
                        }
                        else
                        {
                            Console.Write(" | " + card.Value + "  " + card.Suit.Substring(0, 1) + " | ");
                        }

                    }

                }
                Console.WriteLine();

                for (int i = 0; i < player.Hand.Count; i++)
                {
                    Console.Write(" |______| ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }


        private static void DrawScores()
        {
            foreach (var player in Players)
            {
                Console.WriteLine(player.Name + ": " + player.Score);
            }

            Console.WriteLine("House: " + House.Score);
        }
    }
}