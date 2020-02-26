using System;
using System.Collections.Generic;
using System.Text;

namespace bjdev
{

  public enum PlayerAction { Hit, Stand }

  public class CellStrategyAndResult
  {

    public int numTimesSeen;
    public int hitWins;
    public int standWins;
    public int pushes;
    public PlayerAction currentPlayerAction;

    public CellStrategyAndResult()
    {
      currentPlayerAction = PlayerAction.Hit; // Just hit first
    }

    public PlayerAction WhatDo()
    {
      if (currentPlayerAction == PlayerAction.Hit)
      {
        return PlayerAction.Stand;
      }
      else
      {
        return PlayerAction.Hit;
      }
    }

  }
}
