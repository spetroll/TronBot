using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class MiniMaxNode
    {
        private int player = 0;
        public GameState State;
        private Evaluator e;
        private List<MiniMaxNode> Children;
        private MiniMaxNode Parent;
        public int score;

        public MiniMaxNode(GameState g, Evaluator e, int player)
        {
            this.State = g;
            this.e = e;
            this.player = player;
            Parent = this;

        }

        public MiniMaxNode(GameState g, Evaluator e, int player, MiniMaxNode parent)
        {
            this.State = g;
            this.e = e;
            this.player = player;
            this.Parent = parent;
        }

        public int evaluate(int alpha, int beta)
        {
            //player is always 0
            if (Children == null || Children.Count == 0)
            {
               // int evaluation = e.evaluation(State, 0);
                //Console.Error.WriteLine("MinNode {0}{1} - Depth: {2} Children: {3} - Eval:{4}\n", n.State.Player.X, n.State.Player.Y, Depth, Childen.Count, evaluation);
                return e.evaluation(State, 0);
            }
            //opponent playing, minimize outcome
            if(player == 1)
            {
                foreach (MiniMaxNode n in Children)
                {
                    int evaluation = n.evaluate(alpha, beta);
                    if (evaluation <= alpha) return alpha;
                    if (evaluation < beta) beta = evaluation;
                    //Console.Error.WriteLine("MinNode {0}{1} - Eval:{2}\n", n.State.Player.X, n.State.Player.Y, evaluation); 
                }
                //System.err.println("Min: " + min);
                return beta;
            }
            //player playing, maximize outcome
            if(player == 0)
            {
                foreach(MiniMaxNode n in Children)
                {
                    int evaluation = n.evaluate(alpha, beta);
                    if (evaluation >= beta) return beta;
                    if (evaluation > alpha) alpha = evaluation;
                   // Console.Error.WriteLine("MaxNode {0}{1} - Eval:{2}\n", n.State.Player.X, n.State.Player.Y, evaluation); 
                }
                
                return alpha;
            }
            //we should not get here

            return 0;
        }


        //creates the child nodes and expands the current node with them
        public void expand()
        {
            //next moves will always be the other players moves

            //opponent moves need successors generated from parent because of simulatneous moves
            List<GameState> childStates = new List<GameState>();
            if (player == 0)
            {
                
                childStates = Parent.State.getSuccessorStates((player + 1) % 2);

            }
            else
            {

                childStates = State.getSuccessorStates((player + 1) % 2);
            }
            //foreach (GameState gs in childStates)
            //{
            //    Console.Error.WriteLine(gs.ToString());
            //}
            Children = new List<MiniMaxNode>();
            foreach (GameState g in childStates)
            {
                
                Children.Add(new MiniMaxNode(g, e, (player + 1) % 2, this));
            }
        }

        private void expand(int depth)
        {
            if(depth > 0)
            {
                expand();
                foreach (MiniMaxNode n in Children)
                {
                    n.expand(depth - 1);
                }
            }
        }

        public static MiniMaxNode getGameTree(GameState g, Evaluator e, int player, int depth)
        {
            MiniMaxNode n = new MiniMaxNode(g, e, player);
            n.expand(depth);
            return n;
        }

        public override string ToString()
        {
            //string output = "";
            //if (Children != null && Children.Count > 0)
            //    foreach (MiniMaxNode n in Children)
            //    {
            //        output += "\t" + n.ToString() + "";

            //    }
            return this.State.ToString() + ", Score: " + this.score;
        }
    }
}
