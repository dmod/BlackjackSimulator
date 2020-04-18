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
      Array suits = Enum.GetValues(typeof(Suit));

      foreach (Suit suit in suits)
      {
        cards.Add(new Card(suit, "2", 2));
        cards.Add(new Card(suit, "3", 3));
        cards.Add(new Card(suit, "4", 4));
        cards.Add(new Card(suit, "5", 5));
        cards.Add(new Card(suit, "6", 6));
        cards.Add(new Card(suit, "7", 7));
        cards.Add(new Card(suit, "8", 8));
        cards.Add(new Card(suit, "9", 9));
        cards.Add(new Card(suit, "10", 10));
        cards.Add(new Card(suit, "J", 10));
        cards.Add(new Card(suit, "Q", 10));
        cards.Add(new Card(suit, "K", 10));
        cards.Add(new Card(suit, "A", 1));
      }

    }
  }
}
