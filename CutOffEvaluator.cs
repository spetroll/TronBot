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

            int[,] newMap = MapManipulator.straightLineFromPosition(map, player == 1 ? g.Opponent : g.Player, direction);
            int distance = MapManipulator.distanceStraightLine(map, player == 1 ? g.Opponent : g.Player, direction);
            MapAnalyzer m = new MapAnalyzer(newMap);
            //m.printMap();

            //since MapAnalyzer actually clones the arrays and so player reports the wrong size if being part of the wall, manipulate the array directly
            //and fill opponent first, then check the player field size. this will report correct size
            int opponentFieldSize = MapManipulator.fieldFill(newMap, player == 0 ? g.Opponent : g.Player);
            int playerFieldSize = MapManipulator.fieldFill(newMap, player == 1 ? g.Opponent : g.Player);

            //Console.Error.WriteLine(MyTronBot.intDirectionToString(direction));
            //Console.Error.WriteLine("OpponentFieldSize: " + opponentFieldSize + " PlayerFieldSize: " + playerFieldSize);
            //Console.Error.WriteLine("Utility cutoff: " + (playerFieldSize-opponentFieldSize) + "\n");
            int evaluation = playerFieldSize - opponentFieldSize;

            //now what to do with distance? we want to minimize it but we need higher evaluations for better positions
            //and there is no (obvious) maximum distance. 
            //try array size - length

            int sideLength = (direction == 1 || direction == 3) ? newMap.GetLength(1) : newMap.GetLength(0);

            int distanceEvaluation = sideLength - distance;

            int finalEval = 0;


            //Console.Error.WriteLine("Distance Eval: " + distanceEvaluation + "\nEval: " + evaluation);
            //if (opponentFieldSize < playerFieldSize) finalEval = playerFieldSize + distanceEvaluation;
            if (opponentFieldSize < playerFieldSize) finalEval = playerFieldSize;
            //Console.Error.WriteLine("FinalEval: " + finalEval);
            return finalEval;

            //      if(finalEval > 0) return finalEval;
            //      else return 0;

        }
    }
}
