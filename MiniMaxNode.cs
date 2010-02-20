using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

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
        private double time;
        //private volatile bool timesUp;
        //private double time;

        //public void timedSearch(int depth)
        //{
        //    timesUp = false;
        //    var timer = new Timer(800 - _start);
        //    //timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //    timer.Elapsed += timer_Elapsed;
        //    timer.Start();
        //    expand(depth);
        //    timer.Stop();
        //}


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

        //public int evaluateAlone(int depth)
        //{
        //    if (Children == null || Children.Count == 0)
        //    {
        //        this.score = e.evaluation(State, 0) + depth;
        //        //Console.Error.WriteLine("MinNode {0}{1} - Depth: {2} Children: {3} - Eval:{4}\n", n.State.Player.X, n.State.Player.Y, Depth, Childen.Count, evaluation);
        //        return this.score;
        //    }
        //    foreach (MiniMaxNode n in Children)
        //    {
        //        this.score = n.evaluateAlone(depth+1);
        //        return this.score;
        //    }
        //    return this.score;
        //}

        public int evaluate(int alpha, int beta)
        {
            //player is always 0
            if (Children == null || Children.Count == 0)
            {
                if (State == null) return 0;
                this.score = e.evaluation(State, 0);
                //Console.Error.WriteLine("MinNode {0}{1} - Depth: {2} Children: {3} - Eval:{4}\n", n.State.Player.X, n.State.Player.Y, Depth, Childen.Count, evaluation);
                return this.score;
            }
            //if (alone)
            //{
            //    foreach (MiniMaxNode n in Children)
            //    {
            //        this.score = n.evaluateAlone(0);
            //        return this.score;
            //        //Console.Error.WriteLine("MinNode {0}{1} - Eval:{2}\n", n.State.Player.X, n.State.Player.Y, evaluation); 
            //    }
            //    //System.err.println("Min: " + min);
            //    return beta;
            //}
            //opponent playing, minimize outcome
            if(player == 1)
            {
                foreach (MiniMaxNode n in Children)
                {
                    this.score = n.evaluate(alpha, beta);
                    if (this.score <= alpha) return alpha;
                    if (this.score < beta) beta = this.score;
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
                    this.score = n.evaluate(alpha, beta);
                    if (this.score >= beta) return beta;
                    if (this.score > alpha) alpha = this.score;
                   // Console.Error.WriteLine("MaxNode {0}{1} - Eval:{2}\n", n.State.Player.X, n.State.Player.Y, evaluation); 
                }
                
                return alpha;
            }
            //we should not get here

            return 0;
        }

        public void expand()
        {
            List<GameState> childStates = new List<GameState>();
            Children = new List<MiniMaxNode>();
            //if (!alone)
            //{
                if (player == 0)
                {
                    childStates = Parent.State.getSuccessorStates((player + 1) % 2);
                }
                else
                {
                    childStates = State.getSuccessorStates((player + 1) % 2);
                }

                
                foreach (GameState g in childStates)
                {
                    Children.Add(new MiniMaxNode(g, e, (player + 1) % 2, this));
                }
            //}
            //else
            //{
            //    childStates = State.getSuccessorStates(0);
            //    foreach (GameState g in childStates)
            //    {
            //        Children.Add(new MiniMaxNode(g, e, 0, this, this.alone));
            //    }

            //}
        }

        private void expand(int depth, double usedTime)
        {
            this.time = usedTime;
            //Console.Error.WriteLine("Timer: "+time);
            if (time <= 500 && depth >0)
            {
                HiPerfTimer timer = new HiPerfTimer();
                timer.Start();
                expand();
                foreach (MiniMaxNode n in Children)
                {
                    expand(depth - 1, timer.Duration * 1000);
                }
                timer.Stop();
                time += timer.Duration * 1000;
            }
        }

        public static MiniMaxNode getGameTree(GameState g, Evaluator e, int depth, double usedTime)
        {
            MiniMaxNode n = new MiniMaxNode(g, e, 0);
                n.expand(depth, usedTime);
            return n;
        }

        public override string ToString()
        {
            string output = "";
            if (Children != null && Children.Count > 0)
                foreach (MiniMaxNode n in Children)
                {
                    output += "\t" + n.ToString() + "";

                }
            return this.State.ToString() + ", Score: " + this.score;
        }
    }
}
