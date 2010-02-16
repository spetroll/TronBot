using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class FloodFillEvaluator : Evaluator
    {
        public int evaluation(GameState g, int player)
        {
            MapAnalyzer m = new MapAnalyzer(g);
            int playerFieldSize = m.fieldSize(player == 1 ? g.Opponent : g.Player);
            int opponentFieldSize = m.fieldSize(player == 0 ? g.Opponent : g.Player);
            //Console.Error.WriteLine("Evaluate {0}|{1}: {2} {3}", (player == 1 ? g.Opponent.X : g.Player.X), (player == 1 ? g.Opponent.Y : g.Player.Y), playerFieldSize , opponentFieldSize);
            return playerFieldSize;
        }
    }
}
