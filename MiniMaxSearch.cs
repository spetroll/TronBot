using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class MiniMaxSearch : Search
    {
        public GameState doSearch(GameState g, Evaluator e, int depth, double time)
        {
            HiPerfTimer timer = new HiPerfTimer();

            List<MiniMaxNode> trees = new List<MiniMaxNode>();
            foreach (GameState gs in g.getSuccessorStates(0))
            {
                timer.Start();
                trees.Add(MiniMaxNode.getGameTree(gs, e, depth, time));
                timer.Stop();
                time += timer.Duration * 1000;
            }

            int eval = 0;
            MiniMaxNode bestNode = new MiniMaxNode(g,e,0);
            foreach (MiniMaxNode tree in trees)
            {
                tree.score = tree.evaluate(int.MinValue, int.MaxValue);
                if (tree.score > eval)
                {
                    eval = tree.score;
                    bestNode = tree;
                }
            }

            return bestNode.State;
        }
    }
}