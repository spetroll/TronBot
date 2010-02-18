using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class MiniMaxSearch : Search
    {
        
        

        public GameState doSearch(GameState g, Evaluator e, int player, int depth, bool alone, double time)
        {
            HiPerfTimer timer = new HiPerfTimer();
            HiPerfTimer timer2 = new HiPerfTimer();
           
            List<GameState> states = g.getSuccessorStates(player);
            List<MiniMaxNode> trees = new List<MiniMaxNode>();
            foreach (GameState gs in states)
            {
                timer.Start();
                trees.Add(MiniMaxNode.getGameTree(gs, e, player, depth,alone, time));
                timer.Stop();
                time += timer.Duration * 1000;
            }
            //trees.ForEach(i => Console.Error.WriteLine(i.ToString()));
            int eval = 0;
            MiniMaxNode bestNode = new MiniMaxNode(states[0], e, player);
            foreach (MiniMaxNode tree in trees)
            {
                tree.score = tree.evaluate(int.MinValue, int.MaxValue);
                //Console.Error.WriteLine(tree.ToString());
                if (tree.score > eval)
                {
                    eval = tree.score;
                    bestNode = tree;
                }
            }

            
            //trees.ForEach(i => Console.Error.WriteLine(i.ToString()));
            return bestNode.State;
        }

        #region Search Member

        public GameState doSearch(GameState g, Evaluator e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}