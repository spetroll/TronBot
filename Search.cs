using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public interface Search
    {
        GameState doSearch(GameState g, Evaluator e);
    }
}
