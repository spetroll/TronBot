using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class EvaluatorCollection : Evaluator
    {
        private List<Evaluator> evaluators = new List<Evaluator>();

        public void add(Evaluator e)
        {
            evaluators.Add(e);
        }

        public int evaluation(GameState g, int Player)
        {
            int evaluation = 0;
            foreach (Evaluator e in evaluators)
            {
                evaluation += e.evaluation(g, Player);
            }
            return evaluation;
        }
    }
}
