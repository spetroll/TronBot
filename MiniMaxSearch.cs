using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class MiniMaxSearch : Search
    {
        public GameState doSearch(GameState g, Evaluator e, int player, int depth)
        {
            List<GameState> states = g.getSuccessorStates(player);
            //first get the successors for this move and then make a minimax tree for each separatel            
            //let's see what successor states it actually gives us
            //      for(GameState gS : states){
            //        System.err.println(gS.getPreviousPlayerMove().getDirection());
            //      }
            //now create tree for each state
            List<MiniMaxNode> trees = new List<MiniMaxNode>();
            foreach (GameState gs in states)
            {
                trees.Add(MiniMaxNode.getGameTree(gs, e, player, depth));

            }
            //trees.ForEach(i => i.State.ToString());
            //look for best and choose
            int eval = 0;
            MiniMaxNode bestNode = new MiniMaxNode(states[0], e, player);
            foreach (MiniMaxNode tree in trees)
            {
                tree.score = tree.evaluate(int.MinValue, int.MaxValue);
                if (tree.score > eval)
                {
                    eval = tree.score;
                    bestNode = tree;
                }
            }
            
            //trees.ForEach(i => Console.Error.WriteLine(i.ToString()));
            //      System.err.println(bestNode.getElem().getPreviousPlayerMove().getDirection());
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