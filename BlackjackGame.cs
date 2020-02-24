using System;
using System.Linq;

namespace bjdev
{

    public enum BlackjackResult { PlayerWins, DealerWins }

    public class BlackjackGame
    {

        public FrenchDeck deck;

        public BlackjackGame()
        {
            deck = new FrenchDeck();
        }

        public void StartGame()
        {
            Random rand = new Random();
            var shuffledcards = deck.cards.OrderBy(a => rand.Next()).ToList();

            Console.WriteLine(string.Join(" ", shuffledcards));

            var firstPlayerCard = shuffledcards[0];
            var secondPlayerCard = shuffledcards[2];

            var dealerUpCard = shuffledcards[1];

            // Assume dealer hit on soft 17 rules

            bool shouldHit = PlayerStrategyUtils.FirstAction(firstPlayerCard, secondPlayerCard, dealerUpCard);


        }
    }
}
