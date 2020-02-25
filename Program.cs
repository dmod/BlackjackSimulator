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

            var shoe = BlackjackGame.ShuffleShoe(orderedCards);

            var playerWins = 0;
            var dealerWins = 0;
            var numMatches = 0;

            while (true)
            {
                BlackjackGame bjgame = new BlackjackGame();
                BlackjackResult result = bjgame.PlayGame(shoe);

                numMatches++;

                if (result == BlackjackResult.DealerWins)
                {
                    dealerWins++;
                }
                else if (result == BlackjackResult.PlayerWins)
                {
                    playerWins++;
                }

                double percentWin = (double)playerWins / (double)numMatches;

                double currentShoePenetration = 1 - ((double)shoe.Count / (double)orderedCards.Count);

                Console.WriteLine($"The player has won {playerWins} / {numMatches}. ({percentWin:P5}) Shoe Penetration: {currentShoePenetration:P5}");

                if (currentShoePenetration >= RESHUFFLE_PENETRATION_PERCENT)
                {
                    shoe = BlackjackGame.ShuffleShoe(orderedCards);
                }

            }

        }
    }
}
