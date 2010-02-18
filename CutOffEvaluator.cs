using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class CutOffEvaluator : Evaluator
    {
        public int evaluation(GameState g, int player)
        {
            int direction = g.previousPlayerMove.Direction;
            int[,] map = g.Map;

            //int[,] newMap = MapManipulator.straightLineFromPosition(map, player == 1 ? g.Opponent : g.Player, direction);
            //int distance = MapManipulator.distanceStraightLine(map, player == 1 ? g.Opponent : g.Player, direction);
            MapAnalyzer m = new MapAnalyzer(map);
            //m.printMap();

            //since MapAnalyzer actually clones the arrays and so player reports the wrong size if being part of the wall, manipulate the array directly
            //and fill opponent first, then check the player field size. this will report correct size
            int opponentFieldSize = m.fieldSize(player == 0 ? g.Opponent : g.Player);
            int playerFieldSize = m.fieldSize(player == 1 ? g.Opponent : g.Player);
            bool seperated = m.sameField(g.Player, g.Opponent);
            int score = 0;
            if (seperated && playerFieldSize > opponentFieldSize) return 100;
            if (seperated && opponentFieldSize > playerFieldSize) return -100;
            if (seperated && opponentFieldSize == playerFieldSize) return 5;

            if (g.Player.X + 1 == g.Opponent.X && g.Player.Y == g.Opponent.Y) score -= -50;
            if (g.Player.X - 1 == g.Opponent.X && g.Player.Y == g.Opponent.Y) score -= -50;
            if (g.Player.X == g.Opponent.X && g.Player.Y + 1 == g.Opponent.Y) score -= -50;
            if (g.Player.X == g.Opponent.X && g.Player.Y - 1 == g.Opponent.Y) score -= -50;
            

            //Console.Error.WriteLine(MyTronBot.intDirectionToString(direction));
            //Console.Error.WriteLine("OpponentFieldSize: " + opponentFieldSize + " PlayerFieldSize: " + playerFieldSize);
            //Console.Error.WriteLine("Utility cutoff: " + (playerFieldSize - opponentFieldSize) + "\n");
            score = score + playerFieldSize - opponentFieldSize;

            //Console.Error.WriteLine("Distance Eval: " + distanceEvaluation + "\nEval: " + evaluation);
            //if (opponentFieldSize < playerFieldSize) finalEval = playerFieldSize + distanceEvaluation;

            //Console.Error.WriteLine("FinalEval: " + finalEval);
            return score;

            //      if(finalEval > 0) return finalEval;
            //      else return 0;

        }
    }
}
