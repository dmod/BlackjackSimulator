using System.Collections.Generic;

namespace bjdev
{
  public class PlayerStrategyUtils
  {

    public static Dictionary<int, Dictionary<int, CellStrategyAndResult>> softHandStrategies = new Dictionary<int, Dictionary<int, CellStrategyAndResult>>();
    public static Dictionary<int, Dictionary<int, CellStrategyAndResult>> hardHandStrategies = new Dictionary<int, Dictionary<int, CellStrategyAndResult>>();

    public static (bool shouldPlayerHit, CellStrategyAndResult referencedStrategy) ShouldPlayerHit(List<Card> playerHand, Card dealerUpCard)
    {
      (int value, bool isSoft) = HandUtils.CalculateHandValue(playerHand);

      if (value >= 21)
      {
        return (false, null);
      }

      CellStrategyAndResult strategyToReference;

      if (isSoft)
      {
        if (!softHandStrategies.ContainsKey(dealerUpCard.Value))
        {
          softHandStrategies[dealerUpCard.Value] = new Dictionary<int, CellStrategyAndResult>();
        }

        Dictionary<int, CellStrategyAndResult> playerTotalValueToStrategy = softHandStrategies[dealerUpCard.Value];

        if (!playerTotalValueToStrategy.ContainsKey(value))
        {
          playerTotalValueToStrategy[value] = new CellStrategyAndResult();
        }

        strategyToReference = playerTotalValueToStrategy[value];
      }
      else
      {
        if (!hardHandStrategies.ContainsKey(dealerUpCard.Value))
        {
          hardHandStrategies[dealerUpCard.Value] = new Dictionary<int, CellStrategyAndResult>();
        }

        Dictionary<int, CellStrategyAndResult> playerTotalValueToStrategy = hardHandStrategies[dealerUpCard.Value];

        if (!playerTotalValueToStrategy.ContainsKey(value))
        {
          playerTotalValueToStrategy[value] = new CellStrategyAndResult();
        }

        strategyToReference = playerTotalValueToStrategy[value];
      }

      PlayerAction thingToDo = strategyToReference.WhatDo();

      return (thingToDo == PlayerAction.Hit, strategyToReference);
    }
  }
}
