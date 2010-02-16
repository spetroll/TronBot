using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class WallHugEvaluator : Evaluator
    { 
        public int evaluation(GameState g, int player)
        {
            MapAnalyzer m = new MapAnalyzer(g);
            return m.CountWalls(player == 0 ? g.Player : g.Opponent);
        }
    }
}
