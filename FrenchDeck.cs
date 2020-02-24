using System;
using System.Collections.Generic;

namespace bjdev
{
    public class FrenchDeck
    {
        public List<Card> cards;
        public FrenchDeck()
        {
            cards = new List<Card>();
            var suits = Enum.GetValues(typeof(Suit));
            var numbers = Enum.GetValues(typeof(Number));

            foreach (Suit suit in suits)
            {
                foreach (Number num in numbers)
                {
                    cards.Add(new Card(suit, num));
                }
            }

        }
    }
}
