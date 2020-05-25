using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackPractice
{
    class Player
    {
        public string Name { get; set; }
        public double Points = 100;
        public double BetPoints = 0;

        // List of hands which stores a list of cards
        public List<Hand> Hands = new List<Hand>();

        // Make a hand for a player
        public void MakeHand()
        {
            var newHand = new Hand();
            Hands.Add(newHand);
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
                Face = "Ace",
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

                // Player does not have enough points
                if (betInput > Points)
                {
                    Console.WriteLine("You do not have enough points.");
                }

                // Player did not bet enough points
                if (betInput < Points / 4)
                {
                    var minBetAmount = Points / 4;

                    Console.WriteLine($"You have to bet at least {minBetAmount}!");
                }

                // Player runs out of points
                if (Points <= 0)
                {
                    Console.WriteLine($"You ran out of points. Game Over. Press any key to restart.");
                    Console.ReadKey();
                    program.RestartApplication();
                }

                // Player bets points
                if (betInput <= Points && betInput >= Points / 4)
                {
                    Points -= betInput;
                    BetPoints += betInput;

                    Console.WriteLine($"You bet {BetPoints}");
                    goodBet = true;
                }
            }

            Console.WriteLine($"You have {Points} points left");
        }

        // Player splits their hand into two separate hands
        public void SplitHand(Player player)
        {
            var splitHand = new Hand();

            Hands.Add(splitHand);

            var primaryHand = player.Hands[0];
            var firstCardInPlayerHand = player.Hands[0].CardsInHand[0];

            splitHand.AddCard(firstCardInPlayerHand);
            primaryHand.RemoveCard(firstCardInPlayerHand);

            Points -= BetPoints;
            BetPoints = BetPoints * 2;

            program.PressAnyKeyPrompt("You chose to split. Press any key to continue...");
        }

        // Player doubles their bet
        public void DoubleBet()
        {
            Points -= BetPoints;
            BetPoints = BetPoints * 2;

            Console.WriteLine($"You chose to double your bet.\nYou've been dealt {program.CardAsString(Deck.Cards[0])}\n");
        }

        // Player receives half if their bet back
        public void Surrender(Player player)
        {
            BetPoints = BetPoints / 2;
            Points += BetPoints;

            Console.WriteLine($"You chose to surrender. You will receive {player.BetPoints} back.");
            BetPoints = 0;
        }

        // Player adds calculated bet points to their point.
        public void PlayerWin()
        {
            Points += BetPoints;
            BetPoints = 0;
        }

        // Player receives half of their bet back
        public void PlayerTie()
        {
            Points += BetPoints;
            BetPoints = 0;
        }

        // Player loses their bet
        public void PlayerLose()
        {
            BetPoints = 0;
        }

        // Player loses their bet and their hand gets removed from the game
        public void Bust()
        {
            BetPoints = 0;

            Hands.RemoveAll(hand => hand.HandValueSum() > 21);

            Console.WriteLine($"{Name} have busted. {Name}'s bet goes to the house.");
        }

        // Removes a players hand when they bust on that hand during a split
        public void BustAHand()
        {
            Hands.RemoveAll(hand => hand.HandValueSum() > 21);

            Console.WriteLine($"{Name} have busted on a hand. This hand will be returned to the deck.");
        }

        // When a dealer busts, the player gets paid 1:1
        public void DealerBust(Player player)
        {
            Console.WriteLine($"{player.Name} gets paid {BetPoints * 2}.");
            player.Points += BetPoints * 2;
            player.BetPoints = 0;
        }
    }
}
