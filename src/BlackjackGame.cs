using bjdev.src;
using System;
using System.Collections.Generic;

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

    public BlackjackGameResult PlayGame(Queue<Card> shoe, int bet, IPlayerStrategy playerStrategy)
    {
      Card firstPlayerCard = shoe.Dequeue();
      Card dealerUpCard = shoe.Dequeue();
      Card secondPlayerCard = shoe.Dequeue();

      Console.WriteLine($"Dealer up card: {dealerUpCard}");
      Console.WriteLine($"Player has: {firstPlayerCard} {secondPlayerCard}");

      List<Card> playerHand = new List<Card> { firstPlayerCard, secondPlayerCard };

      if (Utilities.HandIsBlackjack(playerHand))
      {
        int winnings = Convert.ToInt32(bet * Configuration.BLACKJACK_PAYOUT_RATIO);
        Console.WriteLine($"Blackjack! Player wins: {winnings}");
        return new BlackjackGameResult { Winner = BlackjackResultWinner.PlayerWins, EarningsAfterGame = winnings };
      }

      PlayerStrategyUtils.RecommendedPlayerAction thingToDo = playerStrategy.WhatDo(playerHand, dealerUpCard);

      if (thingToDo == PlayerStrategyUtils.RecommendedPlayerAction.Double)
      {
        bet *= 2;
        Card nextCard = shoe.Dequeue();
        playerHand.Add(nextCard);
        // That's it
      }
      else
      {
        while (thingToDo == PlayerStrategyUtils.RecommendedPlayerAction.Hit)
        {
          Card nextCard = shoe.Dequeue();
          playerHand.Add(nextCard);
          thingToDo = playerStrategy.WhatDo(playerHand, dealerUpCard);
        }
      }

      if (Utilities.HandBusted(playerHand))
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

      if (Utilities.HandBusted(dealerHand))
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


  }
}
