using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class ChaseEvaluator : Evaluator
    {

       public int evaluation(GameState g, int player)
       {
           MapAnalyzer m = new MapAnalyzer(g.Map);
            
           int playerFieldSize = m.fieldSize(player == 1 ? g.Opponent : g.Player);
           int opponentFieldSize = m.fieldSize(player == 0 ? g.Opponent : g.Player);

           return playerFieldSize - opponentFieldSize;
            
       }

  

}
}
