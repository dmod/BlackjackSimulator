using System;
using System.Collections.Generic;

namespace bjdev.src
{
  public class StrategyGenerator  
  {
    public static void GenerateStrategy()
    {
      List<Card> orderedCards = new List<Card>();

      for (int deck = 0; deck < Configuration.NUM_DECKS_IN_SHOE; deck++)
      {
        orderedCards.AddRange(new FrenchDeck().cards);
      }

      Queue<Card> shoe = Utilities.ShuffleShoe(orderedCards);

      IPlayerStrategy strategy = new RandomPlayerStrategy();

      for (int gameIndex = 0; gameIndex < Configuration.STRATEGY_GENERATOR_NUM_GAMES; gameIndex++)
      {
        BlackjackGame bjgame = new BlackjackGame();

        int bet = 10;

        BlackjackGameResult result = bjgame.PlayGame(shoe, bet, strategy);

        double currentShoePenetration = 1 - (shoe.Count / (double)orderedCards.Count);

        if (currentShoePenetration >= Configuration.RESHUFFLE_PENETRATION_PERCENT)
        {
          Console.WriteLine("Shuffling shoe");
          shoe = Utilities.ShuffleShoe(orderedCards);
        }
      }




    }
  }
}
