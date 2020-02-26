using System.Collections.Generic;

namespace bjdev
{
  public class PlayerStrategyUtils
  {
    public static bool ShouldPlayerHit(List<Card> playerHand, Card dealerUpCard)
    {
      return !HandUtils.HandHigherThanSoft17(playerHand);
    }
  }
}
