using System;

namespace bjdev
{
    public enum Suit { Heart, Spade, Diamond, Club }
    public class Card
    {
        public Suit Suit;

        public Number Number;

        public static Tuple<string, int>[] ber = { new Tuple<string, int>("Jack", 78) };


        public Card(Suit suit, Number number)
        {
            this.Suit = suit;
            this.Number = number;
        }

        public override string ToString()
        {
            return $"{Suit}{Number}";
        }

    }
}
