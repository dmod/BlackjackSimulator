using System.Collections.Generic;

namespace bjdev
{
  public class PlayerStrategyUtils
  {
    public static bool ShouldPlayerHit(List<Card> playerHand, Card dealerUpCard)
    {
      (int value, bool isSoft) handValue = HandUtils.CalculateHandValue(playerHand);

      if (handValue.value < 22)
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
