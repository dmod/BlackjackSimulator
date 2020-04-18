using System;
using System.Collections.Generic;
using System.Text;
using static bjdev.PlayerStrategyUtils;

namespace bjdev.src
{
  public interface IPlayerStrategy
  {
    RecommendedPlayerAction WhatDo(List<Card> playerHand, Card dealerUpCard);
  }
}
