using System.Collections.Generic;

namespace bjdev
{
    public enum Suit { Heart, Spade, Diamond, Club }

    public class Card
    {

        public Suit Suit;

        public string NumberKey;

        public int Value;


        public Card(Suit suit, string numberKey, int value)
        {
            this.Suit = suit;
            this.NumberKey = numberKey;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{Suit}{NumberKey}";
        }

    }
}
