using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Blackjack
{
    class Program
    {
        //---------------------------------------------------
        // DEAL PHASE

        // Shuffles the deck
        public static void Shuffle()
        {
            // Performs the Fisher-Yates shuffle
            for (var firstIndex = Cards.deck.Count - 1; firstIndex >= 0; firstIndex--)
            {
                var randomNumber = new Random();
                var secondIndex = randomNumber.Next(0, firstIndex);
                var swapCards = Cards.deck[firstIndex];

                Cards.deck[firstIndex] = Cards.deck[secondIndex];
                Cards.deck[secondIndex] = swapCards;
            }
        }

        // Deals the top two cards to the player and computer
        public static void Deal()
        {
            Players.playerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            Players.playerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            Players.computerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            Players.computerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);

            Console.Clear();
            Console.WriteLine($"\n\n\nCards have been dealt. Look at your hand below...");
            Console.WriteLine($"\n\n\n<><><><><>Player Phase<><><><><>");
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllHiddenComputerCards();
        }

        // --------------------------------------------------
        // PLAYER PHASE
        public static void PlayerPhase()
        {
            var goodResponse = false;

            // Loops the player phase of the game until they stand or bust
            while (!goodResponse)
            {
                // Busts the player after hitting
                int playerHandAmount = Players.playerHand.Sum(item => item.Value());
                bool bust = playerHandAmount > 21;
                if (bust)
                {
                    PlayerBust();
                    break;
                }

                // Prints the command to hit or stay in the console and asks for a response
                Console.Write("\nPress \'H\' to hit, \'S\' to Stand: ");
                var input = Console.ReadKey();

                // booleans for a good input for the prompt
                bool hit = input.KeyChar == 'h';
                bool stand = input.KeyChar == 's';
                bool invalidAnswer = input.KeyChar != 'h' || input.KeyChar != 's';

                // Player hits to receive a card in their hand
                if (hit)
                {
                    PlayerHit();
                }
                // Player stands to maintain the value in their posession.
                if (stand)
                {
                    PlayerStand();
                    goodResponse = true;
                }
                // Player enters an invalid input
                if (!goodResponse && !hit)
                {
                    Console.WriteLine("\n{0} is not a valid answer", input.KeyChar);
                }
            }
        }

        // Adds a card to a players hand when the player selects 'hit' during PlayerPhase
        public static void PlayerHit()
        {
            Console.Clear();
            Console.WriteLine($"\n\n\nYou drew a \n{DisplayDrawnCard()}");
            Players.playerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            Console.WriteLine($"\n\n\n<><><><><>Player Phase<><><><><>");
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllHiddenComputerCards();
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"\n\n\n<><><><><>Player Phase<><><><><>");
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllHiddenComputerCards();
        }
        // Prints the command when the player selects 'stand' during the PlayerPhase
        public static void PlayerStand()
        {
            Console.Clear();
            Console.WriteLine($"\n\n\nYou chose to stand");
            Console.Write("\nIt's now the computer's turn. Press any key to start...");
            Console.WriteLine($"\n\n\n<><><><>Player Phase END<><><><>");
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllHiddenComputerCards();
        }
        // Prompts the player to "hit" or "stand"



        //---------------------------------------------------
        // COMPUTER PHASE
        public static void computerPhase()
        {
            Console.Clear();

            bool computerBust = false;
            bool computerStand = false;


            while (!computerBust || !computerStand)
            {

                if (CalculateComputerValueSum() <= 17)
                {
                    ComputerHit();
                }

                if (CalculateComputerValueSum() > 17 && CalculateComputerValueSum() <= 21)
                {
                    ComputerStand();
                    computerStand = true;
                    break;
                }

                if (CalculateComputerValueSum() > 21)

                {
                    ComputerBust();
                    computerBust = true;
                    break;
                }
            }



        }

        // Adds a card to a computers hand if their hand value < 17
        public static void ComputerHit()

        {
            Console.WriteLine($"\n\n\nComputer decided to hit");
            Console.Write("\nPress any key to continue...");
            Console.WriteLine($"\n\n\n<><><><><>Computer Phase<><><><><>");
            Players.computerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllHiddenComputerCards();
            Console.ReadKey();
        }

        // Prints the command when the player selects 'stand' during the PlayerPhase
        public static void ComputerStand()
        {
            Console.Clear();
            Console.WriteLine($"\n\n\nComputer chose to stand");
            Console.WriteLine($"\n\n\n<><><><><>Computer Phase END<><><><><>");
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllHiddenComputerCards();
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();

        }

        //---------------------------------------------------
        // ENDING PHASE


        // Prompts the player that they busted
        public static void PlayerBust()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n!?!?oh oh..!?!?");
            Console.Write("\nPress any key to continue...");
            Console.WriteLine($"\n\n\n<><><><><>SOMEONE BUSTED<><><><><>");
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllComputerCards();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!YOU BUSTED!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU LOSE!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            Console.Write("\nPress any key to try again...");
            Console.ReadKey();
            RestartApplication();
        }

        // Prompts the player that the computer busted
        public static void ComputerBust()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n!?!?oh oh..!?!?");
            Console.Write("\nPress any key to continue...");
            Console.WriteLine($"\n\n\n<><><><><>SOMEONE BUSTED<><><><><>");
            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllComputerCards();
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!YOUR OPPONENT BUSTED!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU WIN!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            Console.Write("\nPress any key to try again...");
            Console.ReadKey();
            RestartApplication();
        }

        public static void PlayerWin()
        {
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU WIN!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            Console.Write("\nPress any key to try again...");
            Console.ReadKey();
            RestartApplication();
        }

        public static void PlayerLose()
        {
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU LOSE!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            Console.Write("\nPress any key to try again...");
            Console.ReadKey();
            RestartApplication();
        }

        public static void Draw()
        {
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!IT'S A DRAW!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            Console.Write("\nPress any key to try again...");
            Console.ReadKey();
            RestartApplication();
        }

        //---------------------------------------------------
        // DISPLAY METHODS

        public static string DisplayDrawnCard()
        {
            return $"{Cards.getDeckRank(0)} of {Cards.getDeckSuit(0)}";
        }




        // Prints the player's card in the console
        public static string DisplayPlayerCard(int playerHandIndex)
        {
            var rank = Players.playerHand[playerHandIndex].Rank;
            var suit = Players.playerHand[playerHandIndex].Suit;
            return rank + " of " + suit;
        }
        // Prints all of the player's card in their hand into the console.
        public static void DisplayAllPlayerCards()
        {
            Console.WriteLine("\nYour hand:");
            for (var i = 0; i < Players.playerHand.Count; i++)
            {
                var rank = Players.playerHand[i].Rank;
                var suit = Players.playerHand[i].Suit;
                Console.WriteLine($"{rank} of {suit}");
            }

        }
        public static void DisplayPlayerValueSum()
        {
            int playerHandAmount;
            playerHandAmount = Players.playerHand.Sum(item => item.Value());
            Console.WriteLine($"Player's hand is valued: {playerHandAmount}");
        }
        public static int CalculatePlayerValueSum()
        {
            int playerHandAmount = Players.playerHand.Sum(item => item.Value());
            return playerHandAmount;

        }

        // Prints the player's card in the console
        public static string DisplayComputerCard(int computerHandIndex)
        {
            var rank = Players.computerHand[computerHandIndex].Rank;
            var suit = Players.computerHand[computerHandIndex].Suit;
            return rank + " of " + suit;
        }

        public static void DisplayAllComputerCards()
        {
            Console.WriteLine("\nTheir hand:");
            for (var i = 0; i < Players.computerHand.Count; i++)
            {

                var rank = Players.computerHand[i].Rank;
                var suit = Players.computerHand[i].Suit;
                Console.WriteLine($"{rank} of {suit}");
            }

        }

        public static void DisplayAllHiddenComputerCards()
        {
            Console.WriteLine("\nTheir hand:");
            for (var i = 0; i < Players.computerHand.Count; i++)
            {

                var rank = Players.computerHand[i].Rank;
                var suit = Players.computerHand[i].Suit;
                rank = "?????";
                suit = "?????";

                Console.WriteLine($"{rank} of {suit}");
            }

        }

        public static void DisplayComputerValueSum()
        {
            int computerHandAmount;
            computerHandAmount = Players.computerHand.Sum(item => item.Value());
            Console.WriteLine($"Computer's hand is valued: {computerHandAmount}");
        }
        public static int CalculateComputerValueSum()
        {
            int computerHandAmount = Players.computerHand.Sum(item => item.Value());
            return computerHandAmount;
        }

        public static void RestartApplication()
        {
            Players.playerHand.Clear();
            Players.computerHand.Clear();
            Console.Clear();
            Main(null);
        }

        //---------------------------------------------------
        // MAIN

        public static void Main(string[] args)
        {
            // List of rank and suit to combine
            var rank = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            var suit = new List<string>() { "Hearts", "Diamonds", "Spades", "Clubs" };

            // Variable to add combined rank and suit as a card
            var card = new Cards();

            // Creates every combination of ranks and suits
            foreach (var selectedRank in rank)
            {
                foreach (var selectedSuit in suit)
                {
                    card.addToDeck(selectedRank, selectedSuit);
                }
            }

            Shuffle();

            Console.Write("Press any key to start...");
            Console.ReadKey();

            Deal();
            PlayerPhase();

            Console.ReadKey();

            computerPhase();



            // END GAME
            Console.Clear();
            Console.WriteLine("\n\nPress any key to reveal the computer's hand...");

            Console.WriteLine("\n\n\n<><><><><><><><><><><><><><><><>");
            Console.WriteLine("<><><><><><>WHO WON?<><><><><><>");
            Console.WriteLine("<><><><><><><><><><><><><><><><>");

            DisplayAllPlayerCards();
            Console.WriteLine($"\n================\n");
            DisplayAllHiddenComputerCards();
            Console.ReadKey();
            Console.Clear();


            DisplayAllPlayerCards();
            DisplayAllComputerCards();
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();


            int playerValueDifference = 21 - CalculatePlayerValueSum();
            int computerValueDifference = 21 - CalculateComputerValueSum();
            if (CalculatePlayerValueSum() == 21 && CalculateComputerValueSum() != 21)
            {
                PlayerWin();
            }
            if (CalculatePlayerValueSum() != 21 && CalculateComputerValueSum() == 21)
            {
                PlayerLose();
            }
            if (playerValueDifference < computerValueDifference)
            {
                PlayerWin();
            }
            if (playerValueDifference > computerValueDifference)
            {
                PlayerLose();
            }
            if (playerValueDifference == computerValueDifference)
            {
                Draw();
            }


        }



    }
}
