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

            bool separated = !ma.sameField(g.Player, g.Opponent);

            EvaluatorCollection ec = new EvaluatorCollection();
            Search s;
            if (!separated)
            {
                ec.add(new CutOffEvaluator());
                ec.add(new VoronoiEvaluator());
                s = new MiniMaxSearch();
            }
            else
            {
                ec.add(new FloodFillEvaluator());
                s = new MiniMaxSearch();
            }
            MultiplyEvaluators finalEval = new MultiplyEvaluators(new GameWinEvaluator(), ec);

            timer.Stop();
            int depth = 4;
            double time = timer.Duration * 1000;
            GameState best = new GameState();
            while (time < 500)
            {
                depth++;
                timer.Start();
                best = new GameState(s.doSearch(g, finalEval, depth, time));
                timer.Stop();
                time += timer.Duration * 1000;
            }
            //Console.Error.WriteLine(separated + " " + time + " " + depth);
            //ma.printMap();

            if (best.previousPlayerMove == null)
                return "N";
            else 
                return intDirectionToString(best.previousPlayerMove.Direction);

        }


        public static void Main()
        {
            try
            {
                while (true)
                {
                    Map.Initialize();
                    Map.MakeMove(MakeMove());
                }
            }
            catch (Exception e)
            {
                Debug.log(e.Message);
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
       
