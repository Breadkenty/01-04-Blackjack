using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackPractice
{
    class Card
    {
        public string Face { get; set; }
        public string Suit { get; set; }
        public int Value(bool aceIs11)
        {
            if (Face == "Ace")
            {
                if (aceIs11)
                {
                    return 11;
                }

                else
                {
                    return 1;
                }
            }

            if (Face == "Jack")
            {
                return 10;
            }

            if (Face == "Queen")
            {
                return 10;
            }

            if (Face == "King")
            {
                return 10;
            }

            else
            {
                return int.Parse(Face);
            }
        }
    }
}
