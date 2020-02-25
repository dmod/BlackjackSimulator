using System;
using System.Collections.Generic;

namespace bjdev
{
    public class PlayerStrategyUtils
    {
        public static bool FirstAction(Card firstPlayerCard, Card secondPlayerCard, Card dealerUpCard)
        {
            if (firstPlayerCard.NumberKey == "A" || secondPlayerCard.NumberKey == "A")
            {
                return SoftFirstAction(firstPlayerCard, secondPlayerCard, dealerUpCard);
            }
            else
            {
                return HardFirstAction(firstPlayerCard, secondPlayerCard, dealerUpCard);
            }
        }

        private static bool SoftFirstAction(Card firstPlayerCard, Card secondPlayerCard, Card dealerUpCard)
        {
            return false;
        }
        public static bool HardFirstAction(Card firstPlayerCard, Card secondPlayerCard, Card dealerUpCard)
        {
            int playerSum = firstPlayerCard.Value + secondPlayerCard.Value;

            if(playerSum <= 8)
            {
                return true;
            }
            return false;

        }

        internal static bool ShouldUserHit(List<Card> userHand, Card dealerUpCard)
        {
            throw new NotImplementedException();
        }
    }
}
