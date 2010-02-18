using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class VoronoiEvaluator : Evaluator
    {
        #region Evaluator Member

        public int evaluation(GameState g, int player)
        {
            MapAnalyzer ma = new MapAnalyzer(g.Map);
            int[] voronoi = MapManipulator.voronoiTerritory(g.Map, player == 0 ? g.Opponent : g.Player, player == 1 ? g.Opponent : g.Player);
            
            return voronoi[1] - voronoi[0];
        }

        #endregion
    }
}
