using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackPractice
{

    class Hand
    {
        // List of cards that are in a hand
        public List<Card> CardsInHand = new List<Card>();

        // Method to accept a card into a hand
        public void AddCard(Card cardToAdd)
        {
            CardsInHand.Add(cardToAdd);
        }

        // Method to remove a card from a hand
        public void RemoveCard(Card cardToRemove)
        {
            CardsInHand.Remove(cardToRemove);
        }

        // Method to calculate the total value of all the cards in a hand (assuming that the ace is an 11)
        public int HandValueSum(Player player)
        {
            return CardsInHand.Sum(cards => cards.Value(true));
        }
    }
}
