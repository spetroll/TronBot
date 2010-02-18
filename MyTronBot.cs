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

            if (g.getSuccessorStates(Player).Count == 0)
            {
                return randomMove();
            }

            int distance = ma.distance(g.Player, g.Opponent);
            bool separated = !ma.sameField(g.Player, g.Opponent);
            
            EvaluatorCollection ec = new EvaluatorCollection();

            if (!separated)
            {
                ec.add(new CutOffEvaluator());
                ec.add(new VoronoiEvaluator());                
            }
            else
            {
                ec.add(new FloodFillEvaluator());
            }
            MultiplyEvaluators finalEval = new MultiplyEvaluators(new GameWinEvaluator(), ec);
            
            
            MiniMaxSearch minmax = new MiniMaxSearch();
            timer.Stop();
            int depth = 1;
            double time = timer.Duration * 1000;
            GameState best = new GameState(minmax.doSearch(g, finalEval, Player, depth, separated, time));
            while (time < 700)
            {
                depth ++;
                timer.Start();
                best = new GameState(minmax.doSearch(g, finalEval, Player, depth, separated, time));
                timer.Stop();
                time += timer.Duration * 1000;
            }
            //Console.Error.WriteLine(separated + " " + time + " " + depth);
            //ma.printMap();
            return intDirectionToString(best.previousPlayerMove.Direction);
            //return "n";


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
       
