using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public static class MapManipulator
    {
        public static int floodFill(int[,] map, Point position)
        {
            return floodFill(map, position.X, position.Y);
        }
        public static int floodFill(int[,] map, int x, int y)
        {
            if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) return 0;
            if (map[x, y] == 0)
            {
                map[x, y] = 5;
                return 1 + floodFill(map, x + 1, y) + floodFill(map, x - 1, y) + floodFill(map, x, y + 1) + floodFill(map, x, y - 1);
            }
            return 0;
        }

        public static int fieldFill(int[,] map, Point position)
        {
            int[,] floodMap = (int[,])map.Clone();
            return 1 + floodFill(floodMap, position.X + 1, position.Y) + floodFill(floodMap, position.X - 1, position.Y) +
                       floodFill(floodMap, position.X, position.Y + 1) + floodFill(floodMap, position.X, position.Y - 1);
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
