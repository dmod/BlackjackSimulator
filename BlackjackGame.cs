using System;
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
    public static Random rand = new Random();

    public static readonly double BLACKJACK_PAYOUT_RATIO = 1.5;

    public BlackjackGameResult PlayGame(Queue<Card> shoe, int bet)
    {
      Card firstPlayerCard = shoe.Dequeue();
      Card dealerUpCard = shoe.Dequeue();
      Card secondPlayerCard = shoe.Dequeue();

      List<Card> playerHand = new List<Card> { firstPlayerCard, secondPlayerCard };

      if (HandIsBlackjack(playerHand))
      {
        return new BlackjackGameResult { Winner = BlackjackResultWinner.PlayerWins, EarningsAfterGame = Convert.ToInt32(bet * BLACKJACK_PAYOUT_RATIO) };
      }

      bool playerShouldHit = PlayerStrategyUtils.ShouldPlayerHit(playerHand, dealerUpCard);
      while (playerShouldHit)
      {
        Card nextCard = shoe.Dequeue();
        playerHand.Add(nextCard);
        playerShouldHit = PlayerStrategyUtils.ShouldPlayerHit(playerHand, dealerUpCard);
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
      return userHand.Count == 2 && HandUtils.CalculateHandValue(userHand).Item1 == 21;
    }

    private bool HandBusted(List<Card> userHand)
    {
      int userHandValue = HandUtils.CalculateHandValue(userHand).Item1;

      if (userHandValue > 21)
      {
        return true;
      }
      return false;
    }

    public static Queue<Card> ShuffleShoe(List<Card> cards)
    {
      IOrderedEnumerable<Card> shuffledcards = cards.OrderBy(x => rand.Next());
      Queue<Card> shoe = new Queue<Card>(shuffledcards);
      return shoe;
    }
  }
}
