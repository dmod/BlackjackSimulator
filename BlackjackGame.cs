using System;
using System.Collections.Generic;
using System.Linq;

namespace bjdev
{
    public enum BlackjackResultWinner { PlayerWins, DealerWins }

    public class BlackjackGameResult
    {
        public BlackjackResultWinner Winner { get; set; }
        public int Winnings { get; set; }
    }

    public class BlackjackGame
    {

        public static Random rand = new Random();

        public BlackjackGameResult PlayGame(Queue<Card> shoe, int bet)
        {
            Card firstPlayerCard = shoe.Dequeue();
            Card dealerUpCard = shoe.Dequeue();
            Card secondPlayerCard = shoe.Dequeue();
            Card dealerDownCard = shoe.Dequeue();

            List<Card> userHand = new List<Card> { firstPlayerCard, secondPlayerCard };

            bool userShouldHit = !HandHigherThanSoft17(userHand);
            while (userShouldHit)
            {
                Card nextCard = shoe.Dequeue();
                userHand.Add(nextCard);
                userShouldHit = !HandHigherThanSoft17(userHand);
            }

            List<Card> dealerHand = new List<Card> { dealerUpCard, dealerDownCard };

            bool dealerShouldStopHitting = HandHigherThanSoft17(dealerHand);
            while (!dealerShouldStopHitting)
            {
                dealerHand.Add(shoe.Dequeue());
                dealerShouldStopHitting = HandHigherThanSoft17(dealerHand);
            }

            var whoWon = CalculateResult(userHand, dealerHand);
            int winnings = 0;
            if (whoWon == BlackjackResultWinner.PlayerWins)
            {
                winnings = bet;
            }
            else if (whoWon == BlackjackResultWinner.DealerWins)
            {
                winnings = -bet;
            }

            return new BlackjackGameResult { Winner = whoWon, Winnings = winnings };
        }

        public bool HandHigherThanSoft17(List<Card> hand)
        {
            Tuple<int, bool> thing = CalculateHandValue(hand);

            if (thing.Item2 == true)
            {
                // Soft
                if (thing.Item1 > 18)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // Hard
                if (thing.Item1 >= 17)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }


        public static BlackjackResultWinner CalculateResult(List<Card> userHand, List<Card> dealerHand)
        {
            int playerValue = CalculateHandValue(userHand).Item1;
            int dealerValue = CalculateHandValue(dealerHand).Item1;

            if (playerValue > 21)
            {
                return BlackjackResultWinner.DealerWins;
            }

            if (dealerValue > 21)
            {
                return BlackjackResultWinner.PlayerWins;
            }

            return playerValue > dealerValue ? BlackjackResultWinner.PlayerWins : BlackjackResultWinner.DealerWins;
        }

        public static Tuple<int, bool> CalculateHandValue(List<Card> hand)
        {
            int value = 0;
            foreach (Card userCard in hand)
            {
                value += userCard.Value;
            }

            // Any benifit from upgrading the Aces to 11's?
            bool isSoft = false;
            foreach (Card userCard in hand)
            {
                if (userCard.NumberKey == "A")
                {
                    if (value + 10 < 22)
                    {
                        value += 10;
                        isSoft = true;
                    }
                }
            }

            return new Tuple<int, bool>(value, isSoft);
        }

        public static Queue<Card> ShuffleShoe(List<Card> cards)
        {
            IOrderedEnumerable<Card> shuffledcards = cards.OrderBy(x => rand.Next());
            Queue<Card> shoe = new Queue<Card>(shuffledcards);
            return shoe;
        }
    }
}
