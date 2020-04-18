using System;
using System.Collections.Generic;
using System.Text;

namespace bjdev.src.PlayerStrategies
{
  public class AlwaysHitPlayerStrategy : IPlayerStrategy
  {
    public PlayerStrategyUtils.RecommendedPlayerAction WhatDo(List<Card> playerHand, Card dealerUpCard)
    {
      return PlayerStrategyUtils.RecommendedPlayerAction.Hit;
    }
  }
}
