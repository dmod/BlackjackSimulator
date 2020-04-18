using System;
using System.Collections.Generic;
using System.Text;

namespace bjdev.src
{
  public class RandomPlayerStrategy : IPlayerStrategy
  {

    public static Random rand = new Random();

    public PlayerStrategyUtils.RecommendedPlayerAction WhatDo(List<Card> playerHand, Card dealerUpCard)
    {
      Array values = Enum.GetValues(typeof(PlayerStrategyUtils.RecommendedPlayerAction));
      PlayerStrategyUtils.RecommendedPlayerAction randomAction = (PlayerStrategyUtils.RecommendedPlayerAction)values.GetValue(rand.Next(values.Length));
      return randomAction;
    }
  }
}
