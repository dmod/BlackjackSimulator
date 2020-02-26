using System;
using System.Collections.Generic;

namespace bjdev
{
  public static class HandUtils
  {
    public static bool HandHigherThanSoft17(List<Card> hand)
    {
      Tuple<int, bool> thing = CalculateHandValue(hand);

      if (thing.Item2 == true)
      {
        // Soft
        if (thing.Item1 > 18)
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
        if (thing.Item1 >= 17)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    public static Tuple<int, bool> CalculateHandValue(List<Card> hand)
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

      return new Tuple<int, bool>(value, isSoft);
    }

  }
}
