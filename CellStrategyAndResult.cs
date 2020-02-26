namespace bjdev
{

  public enum PlayerAction { Hit, Stand }

  public class CellStrategyAndResult
  {

    public int numTimesSeen;
    public int hitWasAGoodChoice;
    public int standWasAGoodChoice;
    public PlayerAction currentPlayerAction;

    public CellStrategyAndResult()
    {
      currentPlayerAction = PlayerAction.Hit; // Just hit first
    }

    public PlayerAction WhatDo()
    {
      numTimesSeen++;
      return currentPlayerAction;
    }

    internal void RecordResult(BlackjackGameResult result)
    {
      switch (result.Winner)
      {
        case BlackjackResultWinner.DealerWins:
          // No action needed
          break;
        case BlackjackResultWinner.PlayerWins:
          if (currentPlayerAction == PlayerAction.Hit)
          {
            hitWasAGoodChoice++;
          }
          else
          {
            standWasAGoodChoice++;
          }
          break;
        case BlackjackResultWinner.Push:
          // No action needed
          break;
      }

      // Do something else next time
      if (currentPlayerAction == PlayerAction.Hit)
      {
        currentPlayerAction = PlayerAction.Stand;
      }
      else
      {
        currentPlayerAction = PlayerAction.Hit;
      }
    }
  }
}
