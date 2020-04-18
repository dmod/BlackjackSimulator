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
  }
}
