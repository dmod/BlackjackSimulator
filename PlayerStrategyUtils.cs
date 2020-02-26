using System.Collections.Generic;

namespace bjdev
{
  public class PlayerStrategyUtils
  {

    public static Dictionary<int, Dictionary<int, CellStrategyAndResult>> softHandStrategies = new Dictionary<int, Dictionary<int, CellStrategyAndResult>>();
    public static Dictionary<int, Dictionary<int, CellStrategyAndResult>> hardHandStrategies = new Dictionary<int, Dictionary<int, CellStrategyAndResult>>();

    public static bool ShouldPlayerHit(List<Card> playerHand, Card dealerUpCard)
    {
      (int value, bool isSoft) = HandUtils.CalculateHandValue(playerHand);

      PlayerAction thingToDo;

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

        thingToDo = playerTotalValueToStrategy[value].WhatDo();
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

        thingToDo = playerTotalValueToStrategy[value].WhatDo();
      }

      return thingToDo == PlayerAction.Hit;
    }
  }
}
