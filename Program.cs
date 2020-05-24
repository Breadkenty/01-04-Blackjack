using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackPractice
{

    class program
    {

        // Selects the primary hand of a player
        public static Hand MainHand(Player player)
        {
            return player.Hands[0];
        }

        // Displays all of the player's cards on the console. Reactive to when a player splits or busts. 
        public static void ShowPlayerCards(List<Player> listOfPlayers)
        {
            foreach (var player in listOfPlayers)
            {
                Console.WriteLine($"\n{player.Name}:\n");

                if (player.Hands.All(hand => hand.HandValueSum(player) > 21))
                {
                    Console.WriteLine("!!BUSTED!!");
                }

                foreach (var hand in player.Hands)
                {

                    if (player.Hands.Count() == 1)
                    {
                        Console.WriteLine($"Hand:");
                    }

                    if (player.Hands.Count() == 2)
                    {
                        Console.WriteLine($"Hand (split):");
                    }

                    foreach (var card in hand.CardsInHand)
                    {
                        Console.WriteLine($"{card.Face} of {card.Suit}");
                        // Console.WriteLine($"Press any key ----------------");
                        // Console.ReadKey();

                    }

                    Console.WriteLine($"Hand value: {hand.HandValueSum(player)}");
                    Console.WriteLine();
                }

                Console.WriteLine($"\nBet: {player.BetPoints}");
                Console.WriteLine($"Points Left: {player.Points}");
                Console.WriteLine($"\n--------------------------------");
            }
        }

        // Shows the dealer's hand where the first card is hidden
        public static void ShowDealerHiddenHand(Player dealer)
        {
            Console.WriteLine($"\n{dealer.Name}'s hand:\n");

            Console.WriteLine($"???? of ????");
            Console.WriteLine($"{MainHand(dealer).CardsInHand[1].Face} of {MainHand(dealer).CardsInHand[1].Suit}");

            Console.WriteLine($"\n--------------------------------");
        }

        // Shows the dealer's hand where both cards are revealed
        public static void ShowDealerRevealedHand(Player dealer)
        {
            Console.WriteLine($"\n{dealer.Name}'s hand:\n");

            Console.WriteLine($"{MainHand(dealer).CardsInHand[0].Face} of {MainHand(dealer).CardsInHand[0].Suit}");
            Console.WriteLine($"{MainHand(dealer).CardsInHand[0].Face} of {MainHand(dealer).CardsInHand[0].Suit}");

            Console.WriteLine($"\n--------------------------------");
        }

        // Returns the cards in the format of a string
        public static string CardAsString(Card card)
        {
            return $"{card.Face} of {card.Suit}";
        }

        // Prompts the user to press a key and returns the input
        public static char PressAKeyPrompt(string prompt)
        {
            Console.WriteLine(prompt);

            var input = Console.ReadKey().KeyChar;

            return input;
        }

        // Prompts the user to press any key to continue
        public static void PressAnyKeyPrompt(string prompt)
        {
            Console.WriteLine(prompt);
            Console.ReadKey();
        }

        // Method for when the game is tied
        public static void Tie(List<Player> players)
        {
            // Returns bet points to total points
            foreach (var player in players)
            {
                player.Points += player.BetPoints;
                player.BetPoints = 0;
            }

            PressAnyKeyPrompt($"The game resulted in a push by {players.First(player => MainHand(player).HandValueSum(player) == 21)}. Press any key to try again.");
            RestartApplication();
        }

        // Method for when the dealer wins the game
        public static void DealerWin(List<Player> players)
        {
            // Bet points are set to 0
            foreach (var player in players)
            {
                player.BetPoints = 0;
            }

            PressAnyKeyPrompt("Dealer wins with a BlackJack. You have lost your bets. Press any key to try again.");
            RestartApplication();
        }

        // Restarts the application when the game is finished
        public static void RestartApplication()
        {
            Console.Clear();
            Main(null);
        }


        static void Main(string[] args)
        {
            // Create dummy players and dealer
            var players = new List<Player>();
            var dealer = new Player
            {
                Name = "Dealer",
            };
            var playerOne = new Player
            {
                Name = "Kento",
                Points = 100
            };
            var playerTwo = new Player
            {
                Name = "Dakota",
                Points = 100
            };
            var playerThree = new Player
            {
                Name = "Joe",
                Points = 100
            };
            var playerFour = new Player
            {
                Name = "Dummy",
                Points = 50
            };

            // Adds created player to list of players


            players.Add(playerFour);
            players.Add(playerOne);
            players.Add(playerTwo);
            players.Add(playerThree);


            // Create a shuffled deck
            var deck = new Deck();
            deck.MakeCards();
            deck.ShuffleCards();

            // Clears the console before starting
            Console.Clear();


            // Initial Phase -------------------------------------------------------------------

            // Each players bet points
            foreach (var player in players)
            {
                player.Bet();
                PressAnyKeyPrompt("Press any key to continue...");
                Console.Clear();
            }

            // ----------------------------------------------------

            // House deals cards to each players
            // foreach (var player in players)
            // {
            //     player.MakeHand(player);
            //     MainHand(player).AddCard(deck.Deal(player));
            //     MainHand(player).AddCard(deck.Deal(player));
            // }

            // For testing - replaced the code above
            // This one does not make a hand or deal cards to the dummy player
            for (var playerIndex = 1; playerIndex < 4; playerIndex++)
            {
                var selectedPlayer = players[playerIndex];

                selectedPlayer.MakeHand(selectedPlayer);

                MainHand(selectedPlayer).AddCard(deck.Deal());
                MainHand(selectedPlayer).AddCard(deck.Deal());
            }

            players[0].MakeDummyHand();

            // ----------------------------------------------------

            // Make a hand for dealer
            dealer.MakeHand(dealer);

            // Cards dealt to dealer
            MainHand(dealer).AddCard(deck.Deal());
            MainHand(dealer).AddCard(deck.Deal());

            // Showing each player's hand
            ShowPlayerCards(players);
            PressAnyKeyPrompt("Press any key to see Dealer's hand...");
            Console.Clear();


            //Show dealer's hand (only one face up)
            ShowDealerHiddenHand(dealer);
            PressAnyKeyPrompt("Press any key to enter player phase...");
            Console.Clear();

            // If dealer's shown card is an Ace, 10, Jack, Queen, or King, the dealer can reveal if it has a blackjack (21).
            // If the dealer has a blackjack and all players do not have a black jack, the player loses their bet and the dealer wins.

            var dealerRevealedCardInHand = dealer.Hands[0].CardsInHand[1];
            bool dealerHasBlackJack = MainHand(dealer).HandValueSum(dealer) == 21;
            bool dealerHasAnAceOrTenOnRevealedHand = dealerRevealedCardInHand.Suit == "Ace" || dealerRevealedCardInHand.Suit == "10" || dealerRevealedCardInHand.Suit == "Jack" || dealerRevealedCardInHand.Suit == "Queen" || dealerRevealedCardInHand.Suit == "King";
            bool ifAnyPlayersHasBlackJack = players.Any(player => MainHand(player).HandValueSum(player) == 21);

            if (dealerHasAnAceOrTenOnRevealedHand)
            {
                //If dealer has a black jack results in a dealer win unless a player also has a black jack resulting in a push(tie).
                if (dealerHasBlackJack)
                {
                    ShowDealerRevealedHand(dealer);

                    if (ifAnyPlayersHasBlackJack)
                    {
                        Tie(players);
                    }
                    else
                    {
                        DealerWin(players);
                    }
                }
            }

            // Player phase -------------------------------------------------------------------
            else
            {
                // Cycles through each players in the player phase
                foreach (var player in players)
                {
                    bool standOrBust = false;

                    while (!standOrBust)
                    {
                        // Clears the console, updates it with new set of hands
                        Console.Clear();
                        ShowDealerHiddenHand(dealer);
                        ShowPlayerCards(players);

                        // Shows who's turn it is
                        Console.WriteLine($"It's {player.Name}'s turn");

                        // Prompts the player with their options
                        var input = PressAKeyPrompt("Press H to hit | Press S to stand | Press / to split | Press d to double | Press - to surrender");
                        bool goodInput = "hs/d-".Contains(input);

                        // Conditions for certain options
                        bool enoughPoints = player.BetPoints <= player.Points && player.Points > 1;
                        bool canSplitHand = MainHand(player).CardsInHand[0].Face == MainHand(player).CardsInHand[1].Face;

                        // Hit
                        if (input == 'h')
                        {
                            Console.WriteLine($"\n{player.Name} has been dealt a {CardAsString(Deck.Cards[0])}\n");

                            player.Hands[0].AddCard(deck.Deal());

                            PressAnyKeyPrompt("Press any key to continue");
                        }

                        // Stand
                        if (input == 's')
                        {
                            PressAnyKeyPrompt("\nYou chose to stand. Press any key to continue");
                            standOrBust = true;
                        }

                        // Split
                        if (input == '/' && canSplitHand == true && enoughPoints == true)
                        {
                            player.SplitHand(player);

                            foreach (var hand in player.Hands)
                            {
                                hand.AddCard(deck.Deal());
                            }
                        }

                        // Split - no matching cards
                        if (input == '/' && canSplitHand == false)
                        {
                            PressAnyKeyPrompt("You do not have any matching cards! You cannot split this round.\nPress any key to continue...");
                        }

                        // Split - not enough points
                        if (input == '/' && canSplitHand == true && enoughPoints == false)
                        {
                            PressAnyKeyPrompt("You do not have enough points to do a split.\nPress any key to continue...");
                        }

                        // Hit after splitting
                        if (player.Hands.Count() > 1)
                        {
                            // Runs through the options on each split hand
                            for (var hand = player.Hands.Count() - 1; hand >= 0; hand--)
                            {
                                bool splitHitting = true;
                                enoughPoints = player.BetPoints <= player.Points && player.Points > 0;

                                // prompts the user with options for each hand
                                while (splitHitting)
                                {
                                    // clears the console and displays updated hands
                                    Console.Clear();
                                    ShowDealerHiddenHand(dealer);
                                    ShowPlayerCards(players);

                                    // Shows the selected player's turn
                                    Console.WriteLine($"It's {player.Name}'s turn");

                                    // Prompts the user with the options
                                    var anotherInput = PressAKeyPrompt($"Hand #{hand + 1}\nPress H to hit | Press D to double down | Press S to stand");
                                    var anotherGoodInput = "hds".Contains(anotherInput);

                                    // Hit on a split
                                    if (anotherInput == 'h')
                                    {
                                        Console.WriteLine($"\n{player.Name} has been dealt a {CardAsString(Deck.Cards[0])}\n");

                                        player.Hands[hand].AddCard(deck.Deal());

                                        // Ending prompt
                                        PressAnyKeyPrompt("Press any key to continue");
                                    }

                                    // Double on a split
                                    if (anotherInput == 'd' && enoughPoints == true)
                                    {
                                        // Doubles the player's bet
                                        player.DoubleBet();

                                        // Deals one card to the hand
                                        player.Hands[hand].AddCard(deck.Deal());

                                        // Ending prompt
                                        PressAnyKeyPrompt("Press any key to continue...");
                                        hand -= 1;
                                        splitHitting = false;
                                    }

                                    // Double on split - not enough points
                                    if (anotherInput == 'd' && enoughPoints == false)
                                    {
                                        PressAnyKeyPrompt("You do not have enough points to double your bet.\nPress any key to continue...");
                                    }

                                    // Stand on split
                                    if (anotherInput == 's')
                                    {
                                        PressAnyKeyPrompt("\nYou chose to stand. Press any key to continue");

                                        hand -= 1;
                                        splitHitting = false;
                                    }

                                    // Invalid input
                                    if (anotherGoodInput == false)
                                    {
                                        Console.WriteLine("Invalid Input!");
                                    }

                                    // Bust
                                    if (player.Hands.Any(hand => hand.HandValueSum(player) > 21))
                                    {
                                        // Bust on one hand to hit on the second
                                        player.BustOneHand(player);

                                        // Ending prompt
                                        PressAnyKeyPrompt($"Press any key to continue...");

                                        hand -= 1;
                                        splitHitting = false;
                                    }
                                }

                                // Ends the player's turn skipping the initial hitting phase below VVV
                                standOrBust = true;
                            }
                        }

                        // Double
                        if (input == 'd' && enoughPoints == true)
                        {
                            // Doubles the bet
                            player.DoubleBet();

                            // Deals one card
                            MainHand(player).AddCard(deck.Deal());

                            // Ending prompt
                            PressAnyKeyPrompt("Press any key to continue...");
                            standOrBust = true;
                        }

                        // Double - not enough points
                        if (input == 'd' && enoughPoints == false)
                        {
                            PressAnyKeyPrompt("You do not have enough points to double your bet.\nPress any key to continue...");
                        }

                        // Surrender
                        if (input == '-')
                        {
                            // Player receives half the bet points back
                            player.Surrender(player);

                            // Ending prompt
                            PressAnyKeyPrompt($"Press any key to continue...");
                            standOrBust = true;
                        }

                        // Invalid input
                        if (!goodInput)
                        {
                            PressAnyKeyPrompt("invalid input! \nPress any key to continue...");
                        }

                        // Bust
                        if (player.Hands.All(hand => hand.HandValueSum(player) > 21))
                        {
                            // Player bust, losing all of their bet points
                            player.Bust(player);

                            PressAnyKeyPrompt($"Press any key to continue...");
                            standOrBust = true;
                        }

                    }

                    // Clears the console before the next player starts the loop again
                    Console.Clear();
                    PressAnyKeyPrompt("Next player!");
                }
            }

            // Player
            //  Properties:
            //  Hand - List of cards
            //  Money - Integer
            //  
            //  Methods:
            //  Bet
            //  Be Dealt
            //  Calculate the value of its hand
            //  Show its hand
            //  Hit
            //  Stand
            //  Bust
            //  Split
            //  Double
            //  Surrender
            //  Add their money
            //  Subtract their money
            //  Win
            //  Lose

            // Dealer
            //  Properties:
            //  Hand - List of cards
            //  
            //  Methods:
            //  Be dealt
            //  Calculate the value of its hand
            //  Show one of the cards in its hand
            //  Reveal its hand
            //  Hit
            //  Stand
            //  Bust

            // Deck
            //  Properties:
            //  List of cards
            //
            //  Methods:
            //  Create a card
            //  Deal a card
            //  Shuffle cards

        }
    }
}
