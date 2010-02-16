using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public interface Evaluator
    {
        int evaluation(GameState g, int player);
    }
}
