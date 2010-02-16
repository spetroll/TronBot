using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class GameWinEvaluator : Evaluator
    {
       public int evaluation(GameState g, int player){
            int winStatus = g.getCurrentResult();
            if (player == MyTronBot.Player && winStatus == 100) return Int16.MaxValue;
            if (player == MyTronBot.Player && winStatus == -50) return -100;
            if (player == MyTronBot.Player && winStatus == 0) return 1;
            if (player == MyTronBot.Opponent && winStatus == -100) return Int16.MaxValue;
            if (player == MyTronBot.Opponent && winStatus == -50) return 50;
            if (player == MyTronBot.Opponent && winStatus == 0) return 1;
            return 0;
       }
    }
}