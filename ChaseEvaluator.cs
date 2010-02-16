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
            int direction = g.previousPlayerMove.Direction;
            int[,] map = g.Map;

            int[,] newMap = MapManipulator.straightLineFromPosition(map,player == 1 ? g.Opponent : g.Player,direction);
            int distance = MapManipulator.distanceStraightLine(map,player == 1 ? g.Opponent : g.Player,direction);
            MapAnalyzer m = new MapAnalyzer(newMap);

            //m.printMap();
            
            int playerFieldSize = m.fieldSize(player == 1 ? g.Opponent : g.Player);
            int opponentFieldSize = m.fieldSize(player == 0 ? g.Opponent : g.Player);

            //Console.Error.WriteLine("Utility cutoff: " + (playerFieldSize-opponentFieldSize));

            int evaluation = playerFieldSize - opponentFieldSize;
            if(evaluation> 0) return evaluation; //magic number here, attention
            else return 0;
            
       }

  

}
}
