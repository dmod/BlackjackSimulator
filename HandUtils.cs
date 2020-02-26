using System.Collections.Generic;

namespace bjdev
{
  public static class HandUtils
  {
    public static bool HandHigherThanSoft17(List<Card> hand)
    {
      (int value, bool isSoft) handValue = CalculateHandValue(hand);

      if (handValue.isSoft == true)
      {
        // Soft
        if (handValue.value > 18)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        // Hard
        if (handValue.value >= 17)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    public static (int value, bool isSoft) CalculateHandValue(List<Card> hand)
    {
      int value = 0;
      foreach (Card userCard in hand)
      {
        value += userCard.Value;
      }

      // Any benifit from upgrading the Aces to 11's?
      bool isSoft = false;
      foreach (Card userCard in hand)
      {
        if (userCard.NumberKey == "A")
        {
          if (value + 10 < 22)
          {
            value += 10;
            isSoft = true;
          }
        }
      }

      return (value, isSoft);
    }

  }
}
