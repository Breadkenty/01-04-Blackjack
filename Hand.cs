using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackPractice
{

    class Hand
    {
        // List of cards that are in a hand
        public List<Card> CardsInHand = new List<Card>();

        // Accept a card into a hand
        public void AddCard(Card cardToAdd)
        {
            CardsInHand.Add(cardToAdd);
        }

        // Remove a card from a hand
        public void RemoveCard(Card cardToRemove)
        {
            CardsInHand.Remove(cardToRemove);
        }

        // Calculate the total value of all the cards in a hand (assuming that the ace is an 11)
        public int HandValueSum()
        {
            int totalValue = 0;

            foreach (var card in CardsInHand)
            {
                totalValue += card.Value();
            }

            // Automatically sets Ace as 11 if total hand value goes over 21
            if (totalValue > 21 && CardsInHand.Any(card => card.Face == "Ace"))
            {
                foreach (var card in CardsInHand.Where(card => card.Face == "Ace"))
                {
                    totalValue -= 10;
                }
            }

            return totalValue;
        }
    }
}
