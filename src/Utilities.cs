using System;
using System.Collections.Generic;
using System.Linq;

namespace bjdev.src
{
  public static class Utilities
  {
    public static Queue<Card> ShuffleShoe(List<Card> cards)
    {
      Random rand = new Random();
      IOrderedEnumerable<Card> shuffledcards = cards.OrderBy(x => rand.Next());
      Queue<Card> shoe = new Queue<Card>(shuffledcards);
      return shoe;
    }

    public static bool HandIsBlackjack(List<Card> userHand)
    {
      return userHand.Count == 2 && HandUtils.CalculateHandValue(userHand).value == 21;
    }

    public static bool HandBusted(List<Card> userHand)
    {
      int userHandValue = HandUtils.CalculateHandValue(userHand).value;

      if (userHandValue > 21)
      {
        return true;
      }
      return false;
    }
  }
}
