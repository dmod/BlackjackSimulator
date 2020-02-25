using System;

namespace bjdev
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var playerWins = 0;
            var dealerWins = 0;
            var numMatches = 0;
            while(true)
            {
                BlackjackGame bjgame = new BlackjackGame();
                BlackjackResult result = bjgame.PlayGame();
                numMatches++;

                if(result == BlackjackResult.DealerWins)
                {
                    dealerWins++;
                }
                else if(result == BlackjackResult.PlayerWins)
                {
                    playerWins++;
                }

                double percentWin = (double) playerWins / (double) numMatches;

                Console.WriteLine($"The player has won {playerWins} / {numMatches}. ({percentWin:P5})");
            }

        }
    }
}
