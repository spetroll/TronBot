using System;
using System.Collections.Generic;
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
