using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Blackjack
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

                if (player.Hands.All(hand => hand.HandValueSum() > 21))
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
                        Console.WriteLine(CardAsString(card));
                    }

                    Console.WriteLine($"Hand value: {hand.HandValueSum()}");
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
            Console.WriteLine($"\nDealer's hand:\n");

            Console.WriteLine($"???? of ????");
            Console.WriteLine(CardAsString(MainHand(dealer).CardsInHand[1]));

            Console.WriteLine($"\n--------------------------------");
        }

        // Shows the dealer's hand where both cards are revealed
        public static void ShowDealerRevealedHand(Player dealer)
        {
            Console.WriteLine($"\nDealer's hand:\n");

            foreach (var card in MainHand(dealer).CardsInHand)
            {
                Console.WriteLine($"{CardAsString(card)}");
            }

            Console.WriteLine($"Hand value: {MainHand(dealer).HandValueSum()}");
            Console.WriteLine();

            Console.WriteLine($"\n--------------------------------");
        }

        // Returns the card in the format of a string
        public static string CardAsString(Card card)
        {
            return $"{card.Face} of {card.Suit}";
        }

        // Method to calculate how players are paid at the end of the game
        public static void PayCondition(Player player, Hand playerHand, Player dealer, Hand dealerHand)
        {
            var playerHandValueDifference = 21 - playerHand.HandValueSum();
            int dealerHandValueDifference = 21 - dealerHand.HandValueSum();
            bool playerHasBlackJack = playerHand.HandValueSum() == 21;
            bool dealerHasBlackJack = dealerHand.HandValueSum() == 21;

            // If the player gets a blackjack and the dealer doesn't, the player gets a 3:2 payout
            if (playerHasBlackJack && !dealerHasBlackJack)
            {
                Console.WriteLine($"{player.Name}, you win! Your bet gets a 3:2 payout.");

                player.BetPoints = player.BetPoints * 2.5;

                PressAnyKeyPrompt("Press any key to continue...");
            }

            // If the player is closer to blackjack than the dealer, the player gets a 1:1 payout
            if (playerHandValueDifference < dealerHandValueDifference && !playerHasBlackJack)
            {
                Console.WriteLine($"{player.Name}, you win! You get a 1:1 payout.");

                player.BetPoints = player.BetPoints * 2;

                PressAnyKeyPrompt("Press any key to continue...");
            }

            // If the dealer is closer to blackjack than the player, the player loses their bet
            if (playerHandValueDifference > dealerHandValueDifference)
            {
                Console.WriteLine($"{player.Name}, you lose! You lose your bet.");

                player.BetPoints = 0;

                PressAnyKeyPrompt("Press any key to continue...");
            }

            // If the player and the dealer has the same value of cards, the player's bet is returned
            if (playerHandValueDifference == dealerHandValueDifference)
            {
                Console.WriteLine($"{player.Name}, you tie! You bet is returned.");

                player.Points += player.BetPoints;
                player.BetPoints = 0;

                PressAnyKeyPrompt("Press any key to continue...");
            }

            // If the player ties with the dealer on a blackjack, the player wins if the amount of cards in any of their hand is less than the dealer's
            if (playerHasBlackJack && dealerHasBlackJack && playerHand.CardsInHand.Count() < dealerHand.CardsInHand.Count())
            {
                Console.WriteLine($"{player.Name}, you tied with the dealer, however you have a stronger blackjack! You get a 3:2 payout.");

                player.BetPoints = player.BetPoints * 2.5;

                PressAnyKeyPrompt("Press any key to continue...");
            }

            // If the player ties with the dealer on a blackjack, the player loses if the amount of cards in any of their hand is more than the dealer's
            if (playerHasBlackJack && dealerHasBlackJack && playerHand.CardsInHand.Count() > dealerHand.CardsInHand.Count())
            {
                Console.WriteLine($"{player.Name}, you tied with the dealer, however you have a weaker blackjack! You lose your bet");

                player.BetPoints = player.BetPoints * 2.5;

                PressAnyKeyPrompt("Press any key to continue...");
            }
        }

        // Ask for a string
        public static string AskForString(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
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

        // Restarts the application when the game is finished
        public static void RestartApplication()
        {
            Console.Clear();
            Main(null);
        }

        static void Main(string[] args)
        {
            // For testing ---------------------------------

            // Create dummy players and dealer
            // var players = new List<Player>();
            // var playerOne = new Player
            // {
            //     Name = "Kento",
            //     Points = 100
            // };
            // var playerTwo = new Player
            // {
            //     Name = "Dakota",
            //     Points = 100
            // };
            // var playerThree = new Player
            // {
            //     Name = "Joe",
            //     Points = 100
            // };
            // var playerFour = new Player
            // {
            //     Name = "Dummy",
            //     Points = 50
            // };

            // Adds created player to list of players
            // players.Add(playerOne);
            // players.Add(playerTwo);
            // players.Add(playerThree);

            // Preparing the game -------------------------------------------------------------------

            // Clears the console before starting
            Console.Clear();

            // Create a shuffled deck
            var deck = new Deck();
            deck.MakeCards();
            deck.ShuffleCards();

            // Creates a dealer
            var dealer = new Player
            {
                Name = "Dealer",
            };

            // Creating players -------------------------------------------------------------------

            // Creates a new list of players
            var players = new List<Player>();

            foreach (var player in players)
            {
                Console.WriteLine(player.Name + player.Points);
            }

            bool addingPlayers = true;
            while (addingPlayers)
            {
                var choice = PressAKeyPrompt("Would you like to add a player? Y or N");
                bool goodInput = "yn".Contains(choice);

                if (choice == 'y')
                {
                    string name = AskForString("Enter a player's name");
                    var newPlayer = new Player()
                    {
                        Name = name
                    };
                    players.Add(newPlayer);
                }

                if (choice == 'n')
                {
                    Console.Clear();
                    PressAnyKeyPrompt("Press any key to begin betting...");
                    addingPlayers = false;
                }

                if (!goodInput)
                {
                    Console.WriteLine("Invalid input!");
                }
            }

            // Betting Phase -------------------------------------------------------------------

            // Each players bet points
            foreach (var player in players)
            {
                player.Bet();

                PressAnyKeyPrompt("Press any key to continue...");
                Console.Clear();
            }

            // ----------------------------------------------------

            // House deals cards to each players
            foreach (var player in players)
            {
                player.MakeHand();
                MainHand(player).AddCard(deck.Deal());
                MainHand(player).AddCard(deck.Deal());
            }

            // For testing - replaced the code above
            // ---------------------------------------------------------------
            // This one does not make a hand or deal cards to the dummy player
            // for (var playerIndex = 1; playerIndex < 4; playerIndex++)
            // {
            //     var selectedPlayer = players[playerIndex];

            //     selectedPlayer.MakeHand(selectedPlayer);

            //     MainHand(selectedPlayer).AddCard(deck.Deal());
            //     MainHand(selectedPlayer).AddCard(deck.Deal());
            // }

            // players[0].MakeDummyHand();
            // ---------------------------------------------------------------

            // Make a hand for dealer
            dealer.MakeHand();

            // Cards dealt to dealer
            MainHand(dealer).AddCard(deck.Deal());
            MainHand(dealer).AddCard(deck.Deal());

            // Showing each player's hand
            ShowPlayerCards(players);

            PressAnyKeyPrompt("Press any key to see Dealer's hand...");
            Console.Clear();

            // Dealer expose phase ----------------------------------------------------

            //Show dealer's hand (only one face up)
            ShowDealerHiddenHand(dealer);
            PressAnyKeyPrompt("Press any key to continue...");


            // If the dealer's shown card is an Ace, 10, Jack, Queen, or King, the dealer can reveal if it has a blackjack (21).
            // If the dealer has a blackjack and all players do not have a black jack, the player loses their bet and the dealer wins.
            var dealerRevealedCardInHand = MainHand(dealer).CardsInHand[1];
            bool dealerHasBlackJack = MainHand(dealer).HandValueSum() == 21;
            bool dealerHasAnAceOrTenOnRevealedHand = dealerRevealedCardInHand.Face == "Ace" || dealerRevealedCardInHand.Face == "10" || dealerRevealedCardInHand.Face == "Jack" || dealerRevealedCardInHand.Face == "Queen" || dealerRevealedCardInHand.Face == "King";
            bool ifAnyPlayersHasBlackJack = players.Any(player => MainHand(player).HandValueSum() == 21);

            if (dealerHasAnAceOrTenOnRevealedHand)
            {
                // If dealer has a blackjack the dealer wins unless a player also has a blackjack resulting in a push(tie) where the game resets.
                if (dealerHasBlackJack)
                {
                    PressAnyKeyPrompt("Press any key to reveal dealer's hand...");
                    Console.Clear();

                    ShowDealerRevealedHand(dealer);
                    PressAnyKeyPrompt("Dealer has a BlackJack. Press any key to continue...");

                    foreach (var player in players)
                    {
                        bool playerHasBlackJack = MainHand(player).HandValueSum() == 21;
                        if (playerHasBlackJack)
                        {
                            player.PlayerTie();
                            PressAnyKeyPrompt($"{player.Name}, you have tied this round by pushing. You get your bet back.");
                        }

                        else
                        {
                            player.PlayerLose();
                            PressAnyKeyPrompt($"{player.Name}, you have lost this round. You have lost your bet.");
                        }
                    }

                    PressAnyKeyPrompt("Press any key to restart...");
                    RestartApplication();
                }

                else
                {
                    PressAnyKeyPrompt("Press any key to reveal dealer's hand...");
                    Console.Clear();

                    ShowDealerRevealedHand(dealer);
                    PressAnyKeyPrompt("Dealer does not have BlackJack. Press any key to enter player phase...");
                };
            }

            // Pause between dealing and player phase
            else
            {
                Console.Clear();
                PressAnyKeyPrompt("Entering player's phase. Press any key to continue...");
            }

            // Player phase -------------------------------------------------------------------

            // Cycles through each players through the player phase
            foreach (var player in players)
            {
                bool standOrBust = false;

                while (!standOrBust)
                {
                    // Clears the console, updates it with new set of hands
                    Console.Clear();

                    if (dealerHasAnAceOrTenOnRevealedHand)
                    {
                        ShowDealerRevealedHand(dealer);
                    }

                    else
                    {
                        ShowDealerHiddenHand(dealer);
                    }

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

                        MainHand(player).AddCard(deck.Deal());

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

                    // Split, no matching cards
                    if (input == '/' && canSplitHand == false)
                    {
                        PressAnyKeyPrompt("You do not have any matching cards! You cannot split this round.\nPress any key to continue...");
                    }

                    // Split, not enough points
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
                            bool secondHit = false;
                            enoughPoints = player.BetPoints <= player.Points && player.Points > 0;

                            // Prompts the user with options for each hand
                            while (splitHitting)
                            {
                                // Clears the console and displays updated hands
                                Console.Clear();

                                if (dealerHasAnAceOrTenOnRevealedHand)
                                {
                                    ShowDealerRevealedHand(dealer);
                                }

                                else
                                {
                                    ShowDealerHiddenHand(dealer);
                                }

                                // Shows the selected player's turn
                                Console.WriteLine($"It's {player.Name}'s turn");

                                // Prompts the user with the options
                                var anotherInput = PressAKeyPrompt($"Hand #{hand}\nPress H to hit | Press D to double down | Press S to stand");
                                var anotherGoodInput = "hds".Contains(anotherInput);

                                // Hit on split
                                if (anotherInput == 'h')
                                {
                                    Console.WriteLine($"\n{player.Name} has been dealt a {CardAsString(Deck.Cards[0])}\n");

                                    player.Hands[hand].AddCard(deck.Deal());

                                    PressAnyKeyPrompt("Press any key to continue");
                                }

                                // Double on split
                                if (anotherInput == 'd' && enoughPoints == true)
                                {
                                    player.DoubleBet();

                                    player.Hands[hand].AddCard(deck.Deal());

                                    PressAnyKeyPrompt("Press any key to continue...");
                                    hand += 1;
                                    splitHitting = false;
                                }

                                // Double on split, not enough points
                                if (anotherInput == 'd' && enoughPoints == false)
                                {
                                    PressAnyKeyPrompt("You do not have enough points to double your bet.\nPress any key to continue...");
                                }

                                // Stand after second hand
                                if (anotherInput == 's' && secondHit)
                                {
                                    PressAnyKeyPrompt("\nYou chose to stand. Press any key to continue");
                                    splitHitting = false;
                                }

                                // Stand on first hand
                                if (anotherInput == 's' && !secondHit)
                                {
                                    PressAnyKeyPrompt("\nYou chose to stand. Press any key to continue");

                                    hand += 1;
                                    secondHit = true;
                                }

                                // Bust on second hand
                                if (player.Hands.Any(hand => hand.HandValueSum() > 21) && secondHit)
                                {
                                    player.BustAHand();

                                    PressAnyKeyPrompt($"Press any key to continue...");
                                    splitHitting = false;
                                }

                                // Bust on one hand
                                if (player.Hands.Any(hand => hand.HandValueSum() > 21))
                                {
                                    player.BustAHand();

                                    hand += 1;
                                    secondHit = true;

                                    PressAnyKeyPrompt($"Press any key to continue...");
                                }

                                // Invalid input
                                if (anotherGoodInput == false)
                                {
                                    Console.WriteLine("Invalid Input!");
                                }
                            }

                        }

                        // Ends the player's split turn skipping the initial hitting phase below
                        standOrBust = true;
                    }

                    // Double
                    if (input == 'd' && enoughPoints == true)
                    {
                        player.DoubleBet();

                        MainHand(player).AddCard(deck.Deal());

                        PressAnyKeyPrompt("Press any key to continue...");
                        standOrBust = true;
                    }

                    // Double, not enough points
                    if (input == 'd' && enoughPoints == false)
                    {
                        PressAnyKeyPrompt("You do not have enough points to double your bet.\nPress any key to continue...");
                    }

                    // Surrender, player receives half the bet points back
                    if (input == '-')
                    {
                        player.Surrender(player);

                        PressAnyKeyPrompt($"Press any key to continue...");
                        standOrBust = true;
                    }

                    // Bust, player loses their bet points
                    if (player.Hands.All(hand => hand.HandValueSum() > 21))
                    {
                        player.Bust();

                        PressAnyKeyPrompt($"Press any key to continue...");
                        standOrBust = true;
                    }

                    // Invalid input
                    if (!goodInput)
                    {
                        PressAnyKeyPrompt("invalid input! \nPress any key to continue...");
                    }
                }
            }

            // Dealer phase -------------------------------------------------------------------

            // show player and dealer hand status 
            Console.Clear();

            if (dealerHasAnAceOrTenOnRevealedHand)
            {
                ShowDealerRevealedHand(dealer);
            }

            else
            {
                ShowDealerHiddenHand(dealer);
            }

            // Dealer's turn to hit and stand
            bool dealerStandOrBust = false;
            while (!dealerStandOrBust)
            {
                // Display dealer's turn
                Console.WriteLine($"It's the dealer's turn");
                PressAnyKeyPrompt("Press any key to continue...");

                // Show live player and dealer hand status
                Console.Clear();
                ShowDealerRevealedHand(dealer);
                ShowPlayerCards(players);

                // Dealer stands
                if (MainHand(dealer).HandValueSum() > 17 && MainHand(dealer).HandValueSum() <= 21)
                {
                    PressAnyKeyPrompt("Dealer chose to stand. Press any key to continue");
                    dealerStandOrBust = true;
                }

                // Dealer busts
                if (MainHand(dealer).HandValueSum() > 21)
                {
                    Console.WriteLine($"The dealer had busted.");

                    // Pays out to each players 1:1 of their bet
                    foreach (var player in players)
                    {
                        player.DealerBust(player);
                    }

                    // Restarts the game
                    PressAnyKeyPrompt("Press any key to play again...");
                    RestartApplication();
                }

                // Dealer hits
                if (MainHand(dealer).HandValueSum() <= 17)
                {
                    Console.WriteLine($"\n{dealer.Name} has been dealt a {CardAsString(Deck.Cards[0])}\n");

                    MainHand(dealer).AddCard(deck.Deal());

                    PressAnyKeyPrompt("Press any key to continue");
                }
            }

            // Ending phase -------------------------------------------------------------------

            foreach (var player in players)
            {
                bool hasAHand = player.Hands.Any(hand => player.Hands.Count() >= 1);

                // If player didn't bust, their payout is calculated
                if (hasAHand)
                {
                    bool playerHasSplitHands = player.Hands.Count() > 1;

                    // If player split their hands
                    if (playerHasSplitHands)
                    {
                        int dealerHandValueDifference = 21 - MainHand(dealer).HandValueSum();
                        var handCloserToBlackJack = player.Hands.FirstOrDefault(hand => 21 - hand.HandValueSum() <= dealerHandValueDifference);

                        PayCondition(player, handCloserToBlackJack, dealer, MainHand(dealer));
                    }

                    // If player didn't split their hands or busted on one split hand
                    else
                    {
                        PayCondition(player, MainHand(player), dealer, MainHand(dealer));
                    }

                }

                // If the player busts, they lose their bet
                else
                {
                    Console.WriteLine($"{player.Name} had busted.");
                    player.BetPoints = 0;

                    PressAnyKeyPrompt("Press any key to continue...");
                }

            }

            Console.Clear();

            // Display payout results and pay players
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} gets paid {player.BetPoints} points");

                player.PlayerWin();
            }

            // Start over
            PressAnyKeyPrompt("Press any key to play again");
            RestartApplication();
        }
    }
}

