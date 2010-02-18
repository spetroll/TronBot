using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class GameState : ICloneable
    {
        public Point Player { get; set; }
        public Point Opponent { get; set; }
        public int[,] Map { get; set; }
        public Move previousPlayerMove = new Move(new Point(1, 1), 1, 0);

        public GameState() { }
        public GameState(int[,] map, Point player, Point opponent)
        {
            this.Map = (int[,])map.Clone();
            this.Player = (Point)player.Clone();
            this.Opponent = (Point)opponent.Clone();
        }
        public GameState(int[,] map, Point player, Point opponent, Move previousplayerMove)
        {
            this.Map = (int[,])map.Clone();
            this.Player = (Point)player.Clone();
            this.Opponent = (Point)opponent.Clone();
            this.previousPlayerMove = previousplayerMove;
        }

        public GameState(GameState g)
        {
            this.Map = (int[,])g.Map.Clone();
            this.Player = (Point)g.Player.Clone();
            this.Opponent = (Point)g.Opponent.Clone();
            this.previousPlayerMove = g.previousPlayerMove;
        }

        public bool isWall(Point position)
        {
            return (Map[position.X, position.Y] == 1);
        }
        public bool isPlayer(Point position)
        {
            return ((Player.X == position.X && Player.Y == position.Y) || (Opponent.X == position.X && Opponent.Y == position.Y));
        }

        public int getCurrentResult()
        {
            MapAnalyzer ma = new MapAnalyzer(Map);
            int playerFields = ma.fieldSize(Player);
            int opponentFields = ma.fieldSize(Opponent);
            if (Player.X+1 == Opponent.X && Player.Y == Opponent.Y) return -10;
            if (Player.X-1 == Opponent.X && Player.Y == Opponent.Y) return -10;
            if (Player.X == Opponent.X && Player.Y+1 == Opponent.Y) return -10;
            if (Player.X == Opponent.X && Player.Y-1 == Opponent.Y) return -10;
            if (playerFields <= 1) return -100;
            if (opponentFields <= 1) return 100;
            return 0;
        }

        public List<GameState> getSuccessorStates()
        {
            
            List<List<Move>> Moves = getValidSuccessorMoves();
            List<GameState> GameStates = new List<GameState>();
            MapAnalyzer ma = new MapAnalyzer(this.Map);
            //ma.printMap();
            foreach (List<Move> moveList in Moves)
            {
                GameStates.Add(getStateAfterMoves(moveList));
            }
            
            return GameStates;
        }
        public List<GameState> getSuccessorStates(int player)
        {
            List<Move> Moves = getValidSuccessorMoves(player);
            List<GameState> GameStates = new List<GameState>();
            foreach (Move move in Moves)
            {
                GameStates.Add(getStateAfterMove(move));
            }
            return GameStates;
        }
        private GameState getStateAfterMoves(List<Move> moveList)
        {
            
            if (moveList.Count == 1) return getStateAfterMove(moveList[0]);
            if (moveList.Count > 1)
            {
                Move first = moveList[0];
                GameState g = getStateAfterMove(first);
                moveList.Remove(first);
                foreach (Move m in moveList)
                {
                    g = g.getStateAfterMove(m);
                }
                
                return g;
            }
            return null;
                    
        }
        private GameState getStateAfterMove(Move m)
        {
            return new GameState(   executeMove((int[,])Map.Clone(), m), 
                                    (m.Player == 0 ? m.Destination : Player),
                                    (m.Player == 1 ? m.Destination : Opponent), 
                                    (m.Player == 0 ? m : previousPlayerMove)
                                    );

        }
        List<List<Move>> getValidSuccessorMoves()
        {
            List<List<Move>> moves = new List<List<Move>>();
            List<int> playerDirections = possibleDirections(Player);
            List<int> opponentDirections = possibleDirections(Opponent);

            int successorStateSize = playerDirections.Count * opponentDirections.Count;

            foreach (int playerPoint in playerDirections)
            {
                foreach (int opponentPoint in opponentDirections)
                {
                    List<Move> successorMoves = new List<Move>();
                    successorMoves.Add(new Move(Player, playerPoint, 0));
                    successorMoves.Add(new Move(Opponent, opponentPoint, 0));

                    moves.Add(successorMoves);
                }
            }

            return moves;
        }
        private List<Move> getValidSuccessorMoves(int player)
        {
            Point playerPoint = (player == 0 ? this.Player : this.Opponent);
            List<int> points = possibleDirections(playerPoint);
            List<Move> moves = new List<Move>();
            foreach (int p in points)
            {
                moves.Add(new Move(playerPoint, p, player));
            }
            return moves;
        }

        private int[,] executeMove(int[,] board, Move m)
        {
            board[m.Destination.X, m.Destination.Y] = 1;
            return board;
        }

        private List<int> possibleDirections(Point Player)
        {
            List<int> directions = new List<int>();
            if (Map[Player.X, Player.Y-1] != 1) directions.Add(1);
            if (Map[Player.X+1, Player.Y] != 1) directions.Add(2);
            if (Map[Player.X, Player.Y+1] != 1) directions.Add(3);
            if (Map[Player.X-1, Player.Y] != 1) directions.Add(4);
            return directions;
        }

        public override string ToString()
        {
            EvaluatorCollection ec = new EvaluatorCollection();
            ec.add(new VoronoiEvaluator());
            Evaluator e = new MultiplyEvaluators(new GameWinEvaluator(), ec);
            //return String.Format("Dir: {6}, PlayerPos: {0}|{1} - Score: {4}, OpponentPos: {2}|{3}, Score: {5}", Player.X, Player.Y, Opponent.X, Opponent.Y
            //                                                                                                    , e.evaluation(this, 0)
            //                                                                                                    , e.evaluation(this, 1)
            //                                                                                                    , MyTronBot.intDirectionToString(previousPlayerMove.Direction)
            //                                                                                                    );
            return String.Format("Dir: {4}, PlayerPos: {0}|{1}, OpponentPos: {2}|{3}", Player.X, Player.Y, Opponent.X, Opponent.Y
                                                                                                                
                                                                                                                , MyTronBot.intDirectionToString(previousPlayerMove.Direction)
                                                                                                                
                                                                                                                );
        }

        #region ICloneable Member

        public object Clone()
        {
            return new GameState(Map, Player, Opponent);
        }

        #endregion
    }
}
