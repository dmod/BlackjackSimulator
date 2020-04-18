using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bjdev
{
  public class AlignmentChart
  {

    public void PrintChart()
    {
      Dictionary<int, Dictionary<int, CellStrategyAndResult>> softHandResults = PlayerStrategyUtils.softHandStrategies;
      Dictionary<int, Dictionary<int, CellStrategyAndResult>> hardHandResults = PlayerStrategyUtils.hardHandStrategies;

      IOrderedEnumerable<int> dealerCardValues = softHandResults.Keys.OrderBy(x=> x);

      foreach(var dealerCardValue in dealerCardValues)
      {
        Console.Write($" {dealerCardValue} ");
      }

    }

  }
}
