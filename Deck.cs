using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Blackjack
{
    class Deck
    {
        // List of cards in a deck
        public static List<Card> Cards = new List<Card>();

        // Makes the set of cards and adds it to the deck.
        public void MakeCards()
        {
            var face = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            var suit = new List<string>() { "Hearts", "Diamonds", "Spades", "Clubs" };

            foreach (string faceSelected in face)
            {
                foreach (string suitSelected in suit)
                {

                    var card = new Card()
                    {
                        Suit = suitSelected,
                        Face = faceSelected
                    };

                    Cards.Add(card);
                }
            }
        }

        // Shuffle the cards in the deck
        public void ShuffleCards()
        {
            // Fisher Yates shuffle
            for (var firstIndex = Deck.Cards.Count - 1; firstIndex >= 1; firstIndex--)
            {
                var randomNumber = new Random();
                var secondCard = randomNumber.Next(firstIndex);
                var firstCard = Deck.Cards[firstIndex];

                Deck.Cards[firstIndex] = Deck.Cards[secondCard];
                Deck.Cards[secondCard] = firstCard;
            }
        }

        // Removes the top card in the deck
        public Card Deal()
        {
            var topCard = Cards[0];
            Cards.Remove(topCard);

            return topCard;
        }
    }
}
