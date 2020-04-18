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
      Suit = suit;
      NumberKey = numberKey;
      Value = value;
    }

    public bool IsAce()
    {
      return NumberKey == "A";
    }

    public override string ToString()
    {
      return $"{NumberKey}{Suit}";
    }

  }
}
