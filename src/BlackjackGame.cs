﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace bjdev
{
  public enum BlackjackResultWinner { PlayerWins, DealerWins, Push }

  public class BlackjackGameResult
  {
    public BlackjackResultWinner Winner { get; set; }
    public int EarningsAfterGame { get; set; }
  }

  public class BlackjackGame
  {
    public static readonly double BLACKJACK_PAYOUT_RATIO = 1.5;

    public BlackjackGameResult PlayGame(Queue<Card> shoe, int bet, out List<CellStrategyAndResult> playerStrategiesReferencesForThisGame)
    {
      playerStrategiesReferencesForThisGame = new List<CellStrategyAndResult>();

      Card firstPlayerCard = shoe.Dequeue();
      Card dealerUpCard = shoe.Dequeue();
      Card secondPlayerCard = shoe.Dequeue();

      List<Card> playerHand = new List<Card> { firstPlayerCard, secondPlayerCard };

      if (HandIsBlackjack(playerHand))
      {
        return new BlackjackGameResult { Winner = BlackjackResultWinner.PlayerWins, EarningsAfterGame = Convert.ToInt32(bet * BLACKJACK_PAYOUT_RATIO) };
      }

      (bool shouldPlayerHit, CellStrategyAndResult referencedStrategy) playerShouldHit = PlayerStrategyUtils.ShouldPlayerHit(playerHand, dealerUpCard);

      playerStrategiesReferencesForThisGame.Add(playerShouldHit.referencedStrategy);
      while (playerShouldHit.shouldPlayerHit)
      {
        Card nextCard = shoe.Dequeue();
        playerHand.Add(nextCard);
        playerShouldHit = PlayerStrategyUtils.ShouldPlayerHit(playerHand, dealerUpCard);
        playerStrategiesReferencesForThisGame.Add(playerShouldHit.referencedStrategy);
      }

      if (HandBusted(playerHand))
      {
        return new BlackjackGameResult { Winner = BlackjackResultWinner.DealerWins, EarningsAfterGame = -bet };
      }

      Card dealerDownCard = shoe.Dequeue();

      List<Card> dealerHand = new List<Card> { dealerUpCard, dealerDownCard };

      bool dealerShouldStopHitting = HandUtils.HandHigherThanSoft17(dealerHand);
      while (!dealerShouldStopHitting)
      {
        dealerHand.Add(shoe.Dequeue());
        dealerShouldStopHitting = HandUtils.HandHigherThanSoft17(dealerHand);
      }

      if (HandBusted(dealerHand))
      {
        return new BlackjackGameResult { Winner = BlackjackResultWinner.PlayerWins, EarningsAfterGame = bet };
      }

      int playerValue = HandUtils.CalculateHandValue(playerHand).value;
      int dealerValue = HandUtils.CalculateHandValue(dealerHand).value;

      if (playerValue == dealerValue)
      {
        return new BlackjackGameResult { Winner = BlackjackResultWinner.Push, EarningsAfterGame = 0 };
      }
      else if (dealerValue > playerValue)
      {
        return new BlackjackGameResult { Winner = BlackjackResultWinner.DealerWins, EarningsAfterGame = -bet };
      }
      else
      {
        return new BlackjackGameResult { Winner = BlackjackResultWinner.PlayerWins, EarningsAfterGame = bet };
      }
    }

    private bool HandIsBlackjack(List<Card> userHand)
    {
      return userHand.Count == 2 && HandUtils.CalculateHandValue(userHand).value == 21;
    }

    private bool HandBusted(List<Card> userHand)
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
