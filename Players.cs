

using System;
using System.Collections.Generic;

namespace _04_Blackjack
{
    class Players
    {
        // List of cards in the player's hand
        public static List<Cards> playerHand = new List<Cards>();

        // List of cards in the computer's hand
        public static List<Cards> computerHand = new List<Cards>();

        // Returns the rank of a card in the player's hand
        public static object getPlayerRank(int deckIndex)
        {
            return playerHand[deckIndex].Rank;
        }

        // Returns the suit of a card in the player's hand
        public static object getPlayerSuit(int deckIndex)
        {
            return playerHand[deckIndex].Suit;
        }

        // Returns the value of a card in the player's hand
        public static int getPlayerValue(int deckIndex)
        {
            return playerHand[deckIndex].Value();
        }

        // Returns the rank of a card in the computer's hand
        public static object getComputerRank(int deckIndex)
        {
            return computerHand[deckIndex].Rank;
        }

        // Returns the suit of a card in the computer's hand
        public static object getComputerSuit(int deckIndex)
        {
            return computerHand[deckIndex].Suit;
        }

        // Returns the value of a card in the computer's hand
        public static object getComputerValue(int deckIndex)
        {
            return computerHand[deckIndex].Value();
        }
    }
}



