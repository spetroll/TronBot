using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class MapAnalyzer
    {
        public int[,] Map { get; set; }

        public MapAnalyzer(int[,] map)
        {
            this.Map = (int[,])map.Clone();
        }
        public MapAnalyzer(GameState g)
        {
            this.Map = (int[,])g.Map.Clone();
        }

        public int CountWalls(int x, int y)
        {
            int temp = 0;

            if (Map[x+1, y] == 1) { temp++; }
            if (Map[x-1, y] == 1) { temp++; }
            if (Map[x, y+1] == 1) { temp++; }
            if (Map[x, y-1] == 1) { temp++; }

            return temp;
        }
        public int CountWalls(Point C)
        {
            return CountWalls(C.X, C.Y);
        }

        public int fieldSize(Point position)
        {
            MapAnalyzer ma = new MapAnalyzer(Map);
            int[,] temp = (int[,])Map.Clone();
            return MapManipulator.floodFill(temp, position);
        }

        public bool sameField(Point pos1, Point pos2)
        {
            MapAnalyzer ma = new MapAnalyzer(Map);
            int pos1size = ma.fieldSize(pos1);
            int pos2size = ma.fieldSize(pos2);
            Console.Error.WriteLine("Pos1: " + pos1size + " Pos2: " + pos2size);
            return (pos1size == pos2size);
        }

        public void printMap()
        {
            int width = Map.GetLength(0);
            int height = Map.GetLength(1);
            Console.Error.WriteLine("MapAnalyzer " + width + " " + height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Error.Write(Map[x, y] == 1 ? '#' : ' ');
                }
                Console.Error.Write('\n');
            }
            Console.Error.Flush();
        }

        public int distance(Point pos1, Point pos2)
        {
            return Math.Abs(pos1.X - pos2.X) + Math.Abs(pos1.Y - pos2.Y);
        }
    }
}
