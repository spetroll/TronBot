using System;
using System.Collections.Generic;

namespace TronBot
{

    class MyTronBot
    {
        public static readonly int Player = 0, Opponent = 1;

        public static String MakeMove()
        {
            HiPerfTimer timer = new HiPerfTimer();
            timer.Start();
            int x = Map.MyLocation.X;
            int y = Map.MyLocation.Y;

            GameState g = new GameState(readMap(), Map.MyLocation, Map.OpponentLocation);
            MapAnalyzer ma = new MapAnalyzer(g);
            List<GameState> succPlayer = g.getSuccessorStates(Player);
           
            //fix that later
            if (succPlayer.Count == 0)
            {
                return randomMove();
            }
            int distance = ma.distance(g.Player, g.Opponent);
            //let's see if they are both separated already
            
            bool separated = !ma.sameField(g.Player, g.Opponent);
            
            EvaluatorCollection ec = new EvaluatorCollection();


            if (!separated && distance > 5)
            {
                 ec.add(new ChaseEvaluator()); 
            }
            else if (!separated && distance <= 5)
            {
                ec.add(new CutOffEvaluator());
            }
            else
            {
                ec.add(new FloodFillEvaluator());
            }
            
            Evaluator finalEval = new MultiplyEvaluators(new GameWinEvaluator(), ec);
            
            MiniMaxSearch minmax = new MiniMaxSearch();
            int depth = 2;
            GameState best = new GameState(minmax.doSearch(g, finalEval, Player, depth));
            // 
            timer.Stop();
            double time = timer.Duration;
            while (time < 0.2)
            {
                timer.Start();
                depth +=2;
                best = minmax.doSearch(g, finalEval, Player, depth);
                timer.Stop();
                time += timer.Duration;
            }
            Console.Error.WriteLine(depth);
            //ma.printMap();
            return intDirectionToString(best.previousPlayerMove.Direction);


        }

        public static void Main()
        {
            while (true)
            {
                Map.Initialize();
                //Timer.Start();
                Map.MakeMove(MakeMove());
                //Timer.Stop();
                //Console.Error.WriteLine("Move: {0:0.0000} ms", Timer.Duration*1000);
            }
        }

        public static List<int> getValidMoves()
        {

            List<int> validMoves = new List<int>();

            int x = Map.MyLocation.X;
            int y = Map.MyLocation.Y;

            if (!Map.IsWall(x, y - 1))
            {
                validMoves.Add(StringDirectionToint("North"));
            }
            if (!Map.IsWall(x + 1, y))
            {
                validMoves.Add(StringDirectionToint("East"));
            }
            if (!Map.IsWall(x, y + 1))
            {
                validMoves.Add(StringDirectionToint("South"));
            }
            if (!Map.IsWall(x - 1, y))
            {
                validMoves.Add(StringDirectionToint("West"));
            }

            return validMoves;

        }

        public static String randomMove()
        {
            if (getValidMoves().Count > 0)
                return intDirectionToString(getValidMoves()[new Random().Next(getValidMoves().Count)]);
            else return "North";
        }

        public static String intDirectionToString(int i)
        {
            switch (i)
            {
                case 1: return "North";
                case 2: return "East";
                case 3: return "South";
                case 4: return "West";
            }
            return null;
        }
        public static int StringDirectionToint(String s)
        {

            if (s == "North") return 1;
            if (s == "East") return 2;
            if (s == "South") return 3;
            if (s == "West") return 4;

            return 0;
        }
        public static int[,] readMap(){

        int[,] map = new int[Map.Width,Map.Height];
        for(int x = 0; x < Map.Width; x++){
          for(int y = 0; y < Map.Height; y++){
              map[x,y] = (Map.IsWall(x,y) ? 1 : 0);
          }
        }
        return map;
    }
    }
}
       
