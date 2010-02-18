using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace TronBot
{
    public static class MapManipulator
    {
        //public static int floodFill(int[,] map, Point position)
        //{
        //    return fieldFill(map, position.X, position.Y);
        //}
        public static int fieldFill(int[,] map, int x, int y)
        {
            if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) return 0;
            if (map[x, y] == 0)
            {
                map[x, y] = 5;
                return 1 + fieldFill(map, x + 1, y) + fieldFill(map, x - 1, y) + fieldFill(map, x, y + 1) + fieldFill(map, x, y - 1);
            }
            return 0;
        }

        public static int floodFill(int[,] map, Point position)
        {
            int[,] floodMap = (int[,])map.Clone();
            return 1 + fieldFill(floodMap, position.X + 1, position.Y) + fieldFill(floodMap, position.X - 1, position.Y) +
                       fieldFill(floodMap, position.X, position.Y + 1) + fieldFill(floodMap, position.X, position.Y - 1);
        }
        public static int[] voronoiTerritory(int[,] map, Point pos1, Point pos2)
        {
            MapAnalyzer ma = new MapAnalyzer(map);
            Queue<Point> q = new Queue<Point>();
            int player = 5;
            int opponent = 3;
            int temp;
            ma.Map[pos1.X, pos1.Y] = 5;
            ma.Map[pos2.X, pos2.Y] = 3;
            q.Enqueue(pos1);
            q.Enqueue(pos2);
            while (q.Count > 0)
            {
                
                Point poz = q.Dequeue();
                foreach (Point pozSuccessor in ma.getNeighbours(poz))
                {
                    if (ma.Map[pozSuccessor.X, pozSuccessor.Y] == 0)
                    {
                        if (ma.Map[poz.X, poz.Y] == 5)
                            ma.Map[pozSuccessor.X, pozSuccessor.Y] = 5;
                        if (ma.Map[poz.X, poz.Y] == 3)
                            ma.Map[pozSuccessor.X, pozSuccessor.Y] = 3;
                        q.Enqueue(pozSuccessor);
                    }
                }
                temp = player;
                player = opponent;
                opponent = temp;
                //ma.printMap();
            }
           // ma.printMap();
            //ma.Map[50,199] = 5;
            int[] result = new int[2];
            result[0] = 0;
            result[1] = 0;
            for (int x = 0; x < ma.Map.GetLength(0); x++)
            {
                for (int y = 0; y < ma.Map.GetLength(1); y++)
                {
                    switch (ma.Map[x, y])
                    {
                        case 5: result[0]++;
                            break;
                        case 3: result[1]++;
                            break;
                        default: break;
                    }
                }
            }
            //Console.Error.WriteLine(result[0] + " " + result[1]);
            return result;
        }

        public static int[,] straightLineFromPosition(int[,] map, Point position, int direction)
        {
            int x = position.X, y = position.Y;
            //Let's clone it first
            int[,] newMap = (int[,])map.Clone();

            bool wallHit = false;

            int i = x, j = y;
            while (!wallHit)
            {

                if (direction == 1)
                    j--;
                else if (direction == 2)
                    i++;
                else if (direction == 3)
                    j++;
                else if (direction == 4)
                    i--;
                if (newMap[i,j] != 1)
                {
                    newMap[i,j] = 1;
                }
                else
                {
                    wallHit = true;
                }
            }
            return newMap;
        }

        public static int distanceStraightLine(int[,] map, Point position, int direction)
        {
            int x = position.X, y = position.Y;
            //Let's clone it first
            int[,] newMap = (int[,])map.Clone();


            bool wallHit = false;
            int length = 0;
            int i = x, j = y;
            while (!wallHit)
            {

                if (direction == 1)
                    j--;
                else if (direction == 2)
                    i++;
                else if (direction == 3)
                    j++;
                else if (direction == 4)
                    i--;
                if (newMap[i,j] != 1)
                {
                    length++;
                }
                else
                {
                    wallHit = true;
                }
            }
            return length;
        }

        



    }
}
