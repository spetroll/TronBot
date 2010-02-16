using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class StatePack
    {
        public List<GameState> gameStates;

        public StatePack(List<GameState> gameStates)
        {
            this.gameStates = gameStates;
        }

        public int averageStateEvaluation(Evaluator e)
        {
            int score = 0;
            foreach (GameState g in gameStates)
            {
                score += e.evaluation(g, MyTronBot.Player);
            }
            return score / gameStates.Count;
        }

    }
}
