using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public interface StateChooser
    {
        GameState choose(List<GameState> states);
    }
}
