using System;
using System.Collections.Generic;

namespace bjdev
{
  public class Program
  {

    public static readonly int NUM_DECKS_IN_SHOE = 6;
    public static readonly double RESHUFFLE_PENETRATION_PERCENT = 0.75;

    public static void Main(string[] args)
    {
      List<Card> orderedCards = new List<Card>();

      for (int deck = 0; deck < NUM_DECKS_IN_SHOE; deck++)
      {
        orderedCards.AddRange(new FrenchDeck().cards);
      }

      Queue<Card> shoe = BlackjackGame.ShuffleShoe(orderedCards);

      int playerWins = 0;
      int dealerWins = 0;
      int numMatches = 0;

      int balance = 100;

      while (true)
      {
        BlackjackGame bjgame = new BlackjackGame();
        int bet = 1;
        BlackjackGameResult result = bjgame.PlayGame(shoe, bet);

        numMatches++;

        if (result.Winner == BlackjackResultWinner.DealerWins)
        {
          dealerWins++;
        }
        else if (result.Winner == BlackjackResultWinner.PlayerWins)
        {
          playerWins++;
        }

        balance += result.EarningsAfterGame;

        double percentWin = playerWins / (double)numMatches;

        double currentShoePenetration = 1 - (shoe.Count / (double)orderedCards.Count);

        Console.WriteLine($"The player has won {playerWins} / {numMatches}. ({percentWin:P5}) Balance: {balance} Shoe Penetration: {currentShoePenetration:P5}");

        if (currentShoePenetration >= RESHUFFLE_PENETRATION_PERCENT)
        {
          shoe = BlackjackGame.ShuffleShoe(orderedCards);
        }
      }
    }
  }
}
