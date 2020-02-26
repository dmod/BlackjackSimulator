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
            Card dealerDownCard = shoe.Dequeue();

            List<Card> playerHand = new List<Card> { firstPlayerCard, secondPlayerCard };

            if (HandIsBlackjack(playerHand))
            {
                return new BlackjackGameResult { Winner = BlackjackResultWinner.PlayerWins, EarningsAfterGame = Convert.ToInt32(bet * BLACKJACK_PAYOUT_RATIO) };
            }

            bool playerShouldHit = !HandHigherThanSoft17(playerHand);
            while (playerShouldHit)
            {
                Card nextCard = shoe.Dequeue();
                playerHand.Add(nextCard);
                playerShouldHit = !HandHigherThanSoft17(playerHand);
            }

            if (HandBusted(playerHand))
            {
                return new BlackjackGameResult { Winner = BlackjackResultWinner.DealerWins, EarningsAfterGame = -bet };
            }

            List<Card> dealerHand = new List<Card> { dealerUpCard, dealerDownCard };

            bool dealerShouldStopHitting = HandHigherThanSoft17(dealerHand);
            while (!dealerShouldStopHitting)
            {
                dealerHand.Add(shoe.Dequeue());
                dealerShouldStopHitting = HandHigherThanSoft17(dealerHand);
            }

            if (HandBusted(dealerHand))
            {
                return new BlackjackGameResult { Winner = BlackjackResultWinner.PlayerWins, EarningsAfterGame = bet };
            }

            int playerValue = CalculateHandValue(playerHand).Item1;
            int dealerValue = CalculateHandValue(dealerHand).Item1;

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
            return userHand.Count == 2 && CalculateHandValue(userHand).Item1 == 21;
        }

        private bool HandBusted(List<Card> userHand)
        {
            int userHandValue = CalculateHandValue(userHand).Item1;

            if (userHandValue > 21)
            {
                return true;
            }
            return false;
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
