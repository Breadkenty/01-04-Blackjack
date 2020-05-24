using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackPractice
{
    class Player
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int BetPoints = 0;

        // List of hands which stores a list of cards
        public List<Hand> Hands = new List<Hand>();

        // Method to make a hand for the player
        public void MakeHand(Player player)
        {
            var newHand = new Hand();
            player.Hands.Add(newHand);
        }

        // Makes a fake hand and custom cards for testing purposes
        public void MakeDummyHand()
        {
            var newDummyHand = new Hand();
            var cardOne = new Card()
            {
                Face = "Ace",
                Suit = "Hearts"
            };
            var cardTwo = new Card()
            {
                Face = "6",
                Suit = "Diamonds"
            };

            Hands.Add(newDummyHand);
            newDummyHand.CardsInHand.Add(cardOne);
            newDummyHand.CardsInHand.Add(cardTwo);
        }

        // Method to bet points 
        public void Bet()
        {
            var goodBet = false;
            while (!goodBet)
            {
                int betInput;

                // Prompts the player to make a bet
                Console.WriteLine($"{Name}, you have {Points} points. Enter the amount you want to bet");
                var goodBetInput = int.TryParse(Console.ReadLine(), out betInput);

                // Not enough points
                if (betInput > Points)
                {
                    Console.WriteLine("You do not have enough points.");
                }

                // Minimum amount of points to bet
                if (betInput < Points / 4)
                {
                    var minBetAmount = Points / 4;
                    Console.WriteLine($"You have to bet at least {minBetAmount}!");
                }

                // Player runs out of points. Restarting the game.
                if (Points <= 0)
                {
                    Console.WriteLine($"You ran out of points. Game Over. Press any key to restart.");
                    Console.ReadKey();
                    program.RestartApplication();
                }

                // Player bet points
                if (betInput <= Points && betInput >= Points / 4)
                {
                    Points -= betInput;
                    BetPoints += betInput;
                    goodBet = true;
                    Console.WriteLine($"You bet {BetPoints}");
                }
            }

            // Shows how many points a player has left
            Console.WriteLine($"You have {Points} points left");
        }

        // Method to split hand
        public void SplitHand(Player player)
        {
            // creates a new hand
            var splitHand = new Hand();

            // Adds the new hand to the list of hands

            Hands.Add(splitHand);

            // Declaring the player's first hand as the primary and the first card in the hand.
            var primaryHand = player.Hands[0];
            var firstCardInPlayerHand = player.Hands[0].CardsInHand[0];


            // in the new hand, take the first card from the first hand

            // Adds the first card in primary hand to the new split hand
            splitHand.AddCard(firstCardInPlayerHand);

            // Removes the card added from the primary hand
            primaryHand.RemoveCard(firstCardInPlayerHand);

            // Doubles the amount of points bet
            Points -= BetPoints;
            BetPoints = BetPoints * 2;

            // Ending prompt
            program.PressAnyKeyPrompt("You chose to split. Press any key to continue...");
        }

        // Method to double the bet
        public void DoubleBet()
        {
            // Doubles the bet
            Points -= BetPoints;
            BetPoints = BetPoints * 2;

            // Ending prompt
            Console.WriteLine($"You chose to double your bet.\nYou've been dealt {program.CardAsString(Deck.Cards[0])}\n");
        }

        // Method to surrender
        public void Surrender(Player player)
        {
            // Returns half the bet points
            BetPoints = BetPoints / 2;
            Points += BetPoints;

            // Ending prompt
            Console.WriteLine($"You chose to surrender. You will receive {player.BetPoints} back.");
            BetPoints = 0;
        }

        // Method for player to bust
        public void Bust(Player player)
        {
            // Player loses all points
            BetPoints = 0;

            // Player loses their hand
            player.Hands.RemoveAll(hand => hand.HandValueSum(player) > 21);

            // Ending prompt
            Console.WriteLine($"You have busted. Your bet goes to the house.");
        }

        // Method for player busting on one hand during a split
        public void BustOneHand(Player player)
        {
            // Player loses a busted hand from their list of hands
            player.Hands.RemoveAll(hand => hand.HandValueSum(player) > 21);

            // Ending prompt
            Console.WriteLine($"You have busted on a hand. This hand will be returned to the deck.");
        }
    }
}
