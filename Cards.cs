using System;
using System.Collections.Generic;

namespace _04_Blackjack
{
    class Cards
    {
        // List of cards as a deck
        public static List<Cards> deck = new List<Cards>();

        // Fetches the rank of the card
        public string Rank { get; set; }

        // Fetches the suit of the card
        public string Suit { get; set; }

        // Calculates the value of the card
        public int Value()
        {
            if (Rank == "Ace")
            {
                return 11;
            }
            if (Rank == "Jack")
            {
                return 10;
            }
            if (Rank == "Queen")
            {
                return 10;
            }
            if (Rank == "King")
            {
                return 10;
            }
            else
            {
                return int.Parse(Rank);
            }
        }

        // Adds the card into the deck
        public void addToDeck(string addSelectedRank, string addSelectedSuit)
        {
            deck.Add(new Cards { Rank = addSelectedRank, Suit = addSelectedSuit });
        }

        // Returns the rank of a card in the deck
        public static object getDeckRank(int deckIndex)
        {
            return deck[deckIndex].Rank;
        }

        // Returns the suit of a card in the deck
        public static object getDeckSuit(int deckIndex)
        {
            return deck[deckIndex].Suit;
        }

        // Returns the value of a card in the deck
        public static int getDeckValue(int deckIndex)
        {
            return deck[deckIndex].Value();
        }
    }
}