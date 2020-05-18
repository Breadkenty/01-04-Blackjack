

using System;
using System.Collections.Generic;

namespace _04_Blackjack
{
    class Players : Cards
    {
        public static List<Cards> playerHand = new List<Cards>();
        public static List<Cards> computerHand = new List<Cards>();

        public static object getPlayerRank(int deckIndex)
        {
            return playerHand[deckIndex].Rank;
        }
        public static object getPlayerSuit(int deckIndex)
        {
            return playerHand[deckIndex].Suit;
        }
        public static int getPlayerValue(int deckIndex)
        {
            return playerHand[deckIndex].Value();
        }

        public static object getComputerRank(int deckIndex)
        {
            return computerHand[deckIndex].Rank;
        }
        public static object getComputerSuit(int deckIndex)
        {
            return computerHand[deckIndex].Suit;
        }
        public static object getComputerValue(int deckIndex)
        {
            return computerHand[deckIndex].Value();
        }


    }

}



