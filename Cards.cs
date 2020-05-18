using System;
using System.Collections.Generic;

namespace _04_Blackjack
{
    class Cards
    {
        public static List<Cards> deck = new List<Cards>();
        public string Rank { get; set; }
        public string Suit { get; set; }
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
        public void addToDeck(string addSelectedRank, string addSelectedSuit)
        {
            deck.Add(new Cards { Rank = addSelectedRank, Suit = addSelectedSuit });
        }
        public static object getDeckRank(int deckIndex)
        {
            return deck[deckIndex].Rank;
        }
        public static object getDeckSuit(int deckIndex)
        {
            return deck[deckIndex].Suit;
        }
        public static int getDeckValue(int deckIndex)
        {
            return deck[deckIndex].Value();
        }
    }

}