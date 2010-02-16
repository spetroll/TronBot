using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class MultiplyEvaluators : Evaluator
    {

        private Evaluator e1, e2;

        public MultiplyEvaluators(Evaluator e1, Evaluator e2) { this.e1 = e1; this.e2 = e2; }

        public int evaluation(GameState g, int player)
        {
            return e1.evaluation(g, player) * e2.evaluation(g, player);
        }
    }
}

