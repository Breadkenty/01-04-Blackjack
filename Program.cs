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
            PressAnyKeyPrompt("Press any key to start...");
        }

        // Deals the top two cards to the player and computer
        public static void Deal()
        {
            // Function
            Players.playerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            Players.playerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            Players.computerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);
            Players.computerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);

            // Console
            Console.Clear();
            DisplayMessage("Cards have been dealt. Look at your hand below...");
            DisplayPhase("Player Phase");
            DisplayHandStatus();
            PressAnyKeyPrompt("Press any key to continue");
        }

        // PLAYER PHASE --------------------------------------------------||

        // Prompts the player with the Hit/Stand loop
        public static void PlayerPhase()
        {
            var goodResponse = false;

            // Loops the player phase of the game until they stand or bust
            while (!goodResponse)
            {
                // Player busts after hit and value is greater than 21
                // Bust is declared first to prevent the Hit/Stand prompt to show when a player busts
                int playerHandAmount = Players.playerHand.Sum(item => item.Value());
                bool bust = playerHandAmount > 21;
                if (bust)
                {
                    PlayerBust();
                    break;
                }

                // Console
                Console.Clear();
                DisplayMessage("Would you like to hit or stand?");
                DisplayPhase("Player Phase");
                DisplayHandStatus();
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

        // Adds a card to a players hand when the player selects 'hit' during Player Phase
        public static void PlayerHit()
        {
            // Consoled before the function to show the card before it is removed from the deck
            Console.Clear();
            DisplayMessage($"You drew a {DisplayDrawnCard()}");

            // Function
            Players.playerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);

            // Console 
            DisplayPhase("Player Phase");
            DisplayHandStatus();
            PressAnyKeyPrompt("Press any key to continue...");
        }

        // Prints the command when the player selects 'stand' during the PlayerPhase
        public static void PlayerStand()
        {
            // Console
            Console.Clear();
            DisplayMessage("You chose to stand. It's now the computer's turn");
            DisplayPhase("Player Phase END");
            DisplayHandStatus();
            PressAnyKeyPrompt("Press any key to continue...");
        }

        // COMPUTER PHASE ---------------------------------------------------||

        // Takes the algorithm for how the computer will make decisions.
        public static void ComputerPhase()
        {
            Console.Clear();

            bool computerBust = false;
            bool computerStand = false;

            // Loops the computer to make a decision between Hit and Stand.

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

        // Adds a card to a computer's hand
        public static void ComputerHit()
        {
            // Function
            Players.computerHand.Add(Cards.deck[0]);
            Cards.deck.Remove(Cards.deck[0]);

            // Console
            Console.Clear();
            DisplayMessage("Computer decided to hit");
            DisplayPhase("Computer Phase");
            DisplayHandStatus();
            PressAnyKeyPrompt("Press any key to continue...");
        }

        // Prints the command when the Computer decides to stand
        public static void ComputerStand()
        {
            Console.Clear();
            DisplayMessage("Computer decided to stand");
            DisplayPhase("Computer Phase");
            DisplayHandStatus();
            PressAnyKeyPrompt("Press any key to continue...");
        }

        // ENDING PHASE ---------------------------------------------------||

        //Begins the ending phase of the game.
        public static void EndingPhase()
        {
            //Console 1
            Console.Clear();
            DisplayMessage("Time to reveal the computer's hand...");
            DisplayPhase("WHO WON?");
            DisplayHandStatus();
            PressAnyKeyPrompt("Press any key to reveal...");

            // Console 2
            Console.Clear();
            DisplayMessage("Here is the computer's hand...");
            DisplayPhase("WHO WON?");
            DisplayRevealedHandStatus();
            PressAnyKeyPrompt("Press any key to continue...");
        }

        // Calculate the value difference to determine who wins.
        public static void CalculateResults()
        {
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

        // Prompts the player that they busted
        public static void PlayerBust()
        {
            // Console 1
            Console.Clear();
            DisplayMessage("oh oh..!?");
            DisplayPhase("SOMEONE BUSTED");
            DisplayRevealedHandStatus();
            PressAnyKeyPrompt("Press any key to continue...");

            // Console 2
            Console.Clear();
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!YOU BUSTED!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU LOSE!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            PressAnyKeyPrompt("Press any key to try again...");

            // Restarts the application
            RestartApplication();
        }

        // Prompts the player that the computer busted
        public static void ComputerBust()
        {
            // Console 1
            Console.Clear();
            DisplayMessage("oh oh..!?");
            DisplayPhase("SOMEONE BUSTED");
            DisplayRevealedHandStatus();
            PressAnyKeyPrompt("Press any key to continue...");

            // Console 2
            Console.Clear();
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!YOUR OPPONENT BUSTED!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU WIN!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            PressAnyKeyPrompt("Press any key to try again...");

            // Restarts the application
            RestartApplication();
        }

        // Prompts the player that they won
        public static void PlayerWin()
        {
            // Console
            Console.Clear();
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU WIN!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            PressAnyKeyPrompt("Press any key to try again...");

            //Restarts the application
            RestartApplication();
        }

        // Prompts the player they lost
        public static void PlayerLose()
        {
            // Console
            Console.Clear();
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!YOU LOSE!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            PressAnyKeyPrompt("Press any key to try again...");

            // Restarts the application
            RestartApplication();
        }

        //Prompts the player that it was a draw
        public static void Draw()
        {
            // Console
            Console.Clear();
            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!IT'S A DRAW!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            PressAnyKeyPrompt("Press any key to try again...");

            //Restarts the application
            RestartApplication();
        }

        // DISPLAY METHODS ---------------------------------------------------||

        //Returns the drawn card from the deck as a string in the format "4 of Spades"
        public static string DisplayDrawnCard()
        {
            return $"{Cards.getDeckRank(0)} of {Cards.getDeckSuit(0)}";
        }

        // Returns a card from the player's hand as a string in the format "4 of Spades"
        public static string DisplayPlayerCard(int playerHandIndex)
        {
            var rank = Players.playerHand[playerHandIndex].Rank;
            var suit = Players.playerHand[playerHandIndex].Suit;
            return rank + " of " + suit;
        }

        // Returns a card from the computer's hand as a string in the format "4 of Spades"
        public static string DisplayComputerCard(int computerHandIndex)
        {
            var rank = Players.computerHand[computerHandIndex].Rank;
            var suit = Players.computerHand[computerHandIndex].Suit;
            return rank + " of " + suit;
        }



        // Prints all of the player's card in their hand into the console.
        public static void DisplayAllPlayerCards()
        {
            Console.WriteLine("Your hand:");
            for (var i = 0; i < Players.playerHand.Count; i++)
            {
                var rank = Players.playerHand[i].Rank;
                var suit = Players.playerHand[i].Suit;
                Console.WriteLine($"{rank} of {suit}");
            }
        }

        // Prints all of the computer's card in their hand into the console.
        public static void DisplayAllComputerCards()
        {
            Console.WriteLine("Their hand:");
            for (var i = 0; i < Players.computerHand.Count; i++)
            {

                var rank = Players.computerHand[i].Rank;
                var suit = Players.computerHand[i].Suit;
                Console.WriteLine($"{rank} of {suit}");
            }
        }

        // Prints all of the computer's card in their hand as a ????? into the console
        public static void DisplayAllHiddenComputerCards()
        {
            Console.WriteLine("Their hand:");
            for (var i = 0; i < Players.computerHand.Count; i++)
            {
                var rank = Players.computerHand[i].Rank;
                var suit = Players.computerHand[i].Suit;
                rank = "?????";
                suit = "?????";

                Console.WriteLine($"{rank} of {suit}");
            }
        }

        // Prints the total value in the player's hand
        public static void DisplayPlayerValueSum()
        {
            int playerHandAmount;
            playerHandAmount = Players.playerHand.Sum(item => item.Value());
            Console.WriteLine($"Player's hand is valued: {playerHandAmount}");
        }

        // Prints the total value in the computer's hand
        public static void DisplayComputerValueSum()
        {
            int computerHandAmount;
            computerHandAmount = Players.computerHand.Sum(item => item.Value());
            Console.WriteLine($"Computer's hand is valued: {computerHandAmount}");
        }

        // Displays the main message into the console
        public static void DisplayMessage(string prompt)
        {
            Console.Write($"\n\n\n{prompt}");
        }

        // Displays the phase into the console
        public static void DisplayPhase(string prompt)
        {
            Console.Write($"\n\n\n<><><><><>{prompt}<><><><><>\n\n\n");
        }

        //Displays both the player's hand and computer's hidden hand into the console
        public static void DisplayHandStatus()
        {
            DisplayAllPlayerCards();
            Console.WriteLine($"\n\n================\n\n");
            DisplayAllHiddenComputerCards();
        }

        //Displays both the player's hand and computer's hand into the console
        public static void DisplayRevealedHandStatus()
        {
            DisplayAllPlayerCards();
            Console.WriteLine($"\n\n================\n\n");
            DisplayAllComputerCards();
        }

        // Displays the "Press any key" prompt into the console
        public static void PressAnyKeyPrompt(string prompt)
        {
            Console.Write($"\n{prompt}");
            Console.ReadKey();
        }

        // Return a integer of the total value in the player's hand
        public static int CalculatePlayerValueSum()
        {
            int playerHandAmount = Players.playerHand.Sum(item => item.Value());
            return playerHandAmount;
        }

        // Returns a integer of the total value in the computer's hand
        public static int CalculateComputerValueSum()
        {
            int computerHandAmount = Players.computerHand.Sum(item => item.Value());
            return computerHandAmount;
        }

        // Clears the player and computer hands and restarts the application.
        public static void RestartApplication()
        {
            Players.playerHand.Clear();
            Players.computerHand.Clear();
            Console.Clear();
            Main(null);
        }

        // MAIN ---------------------------------------------------
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

            // Calling methods for each phases
            Shuffle();
            Deal();
            PlayerPhase();
            ComputerPhase();
            EndingPhase();
            CalculateResults();
        }
    }
}
