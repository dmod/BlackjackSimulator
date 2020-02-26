using System.Collections.Generic;

namespace bjdev
{
  public class PlayerStrategyUtils
  {
    public static bool ShouldPlayerHit(List<Card> playerHand, Card dealerUpCard)
    {
      (int value, bool isSoft) = HandUtils.CalculateHandValue(playerHand);

      if (value < 22)
      {
        return true;
      }
      else
      {
        return false;
      }

      //return !HandUtils.HandHigherThanSoft17(playerHand);
    }
  }
}
