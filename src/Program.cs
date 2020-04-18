using bjdev.src;
using bjdev.src.PlayerStrategies;
using System;
using System.Collections.Generic;

namespace bjdev
{
  public class Program
  {

    public static void Main(string[] args)
    {
      List<Card> orderedCards = new List<Card>();

      for (int deck = 0; deck < Configuration.NUM_DECKS_IN_SHOE; deck++)
      {
        orderedCards.AddRange(new FrenchDeck().cards);
      }

      Queue<Card> shoe = Utilities.ShuffleShoe(orderedCards);

      int playerWins = 0;
      int dealerWins = 0;
      int numPushes = 0;
      int numMatches = 0;

      int balance = 100;

      IPlayerStrategy playerStrategy = new AlwaysHitPlayerStrategy();

      for (int gameIndex = 0; gameIndex < 1000000; gameIndex++)
      {
        BlackjackGame bjgame = new BlackjackGame();
        int bet = 10;
        BlackjackGameResult result = bjgame.PlayGame(shoe, bet, playerStrategy);

        numMatches++;

        switch (result.Winner)
        {
          case BlackjackResultWinner.DealerWins:
            dealerWins++;
            break;
          case BlackjackResultWinner.PlayerWins:
            playerWins++;
            break;
          case BlackjackResultWinner.Push:
            numPushes++;
            break;
        }

        balance += result.EarningsAfterGame;

        double percentWin = playerWins / (double)numMatches;

        double currentShoePenetration = 1 - (shoe.Count / (double)orderedCards.Count);

        Console.WriteLine($"The player has won {playerWins} / {numMatches}. ({percentWin:P5}) ({numPushes} pushes) Balance: {balance} Shoe Penetration: {currentShoePenetration:P5}");

        if (currentShoePenetration >= Configuration.RESHUFFLE_PENETRATION_PERCENT)
        {
          Console.WriteLine("Shuffling shoe");
          shoe = Utilities.ShuffleShoe(orderedCards);
        }
      }
    }
  }
}
