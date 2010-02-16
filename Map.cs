using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class Map
    {
        public static bool[,] Walls { get; set; }
        public static Point MyLocation { get; set; }
        public static Point OpponentLocation { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static bool IsWall(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return true;
            }
            else
            {
                return Walls[x, y];
            }
        }
        public static bool IsWall(Point c)
        {
            return IsWall(c.X, c.Y);
        }

        public static void MakeMove(string direction)
        {
            if (direction.Length <= 0)
            {
                Console.Error.WriteLine("FATAL ERROR: empty direction string. You " +
                           "must specify a valid direction in which " +
                           "to move.");
                Environment.Exit(1);
            }
            string temp = direction.Substring(0, 1).ToUpper();
            int firstChar = (int)temp[0];

            switch (firstChar)
            {
                case 'N':
                    MakeMove(1);
                    break;
                case 'E':
                    MakeMove(2);
                    break;
                case 'S':
                    MakeMove(3);
                    break;
                case 'W':
                    MakeMove(4);
                    break;
                default:
                    Console.Error.WriteLine("FATAL ERROR: invalid move string. The string must " +
                               "begin with one of the characters 'N', 'E', 'S', or " +
                               "'W' (not case sensitive).");
                    Environment.Exit(1);
                    break;
            }
        }

        private static void MakeMove(int direction)
        {
            Console.WriteLine(direction);
        }

        // Reads the map from standard input (from the console).
        public static void Initialize()
        {
            string firstLine = "";
            try
            {
                int c;
                while ((c = Console.Read()) >= 0)
                {
                    if (c == '\n')
                    {
                        break;
                    }
                    firstLine += (char)c;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Could not read from stdin.");
                Environment.Exit(1);
            }
            firstLine = firstLine.Trim();
            if (firstLine.Equals("") || firstLine.Equals("exit"))
            {
                Environment.Exit(1); // If we get EOF or "exit" instead of numbers
                // on the first line, just exit. Game is over.
            }
            string[] tokens = firstLine.Split(' ');
            if (tokens.Length != 2)
            {
                Console.Error.WriteLine("FATAL ERROR: the first line of input should " +
                           "be two integers separated by a space. " +
                           "Instead, got: " + firstLine);
                Environment.Exit(1);
            }
            try
            {
                Width = Convert.ToInt32(tokens[0]);
                Height = Convert.ToInt32(tokens[1]);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("FATAL ERROR: invalid map dimensions: " +
                           firstLine);
                Environment.Exit(1);
            }
            Walls = new bool[Width, Height];
            bool foundMyLocation = false;
            bool foundHisLocation = false;
            int numSpacesRead = 0;
            int x = 0, y = 0;
            while (y < Height)
            {
                int c = 0;
                try
                {
                    c = Console.Read();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("FATAL ERROR: exception while reading " +
                               "from stdin.");
                    Environment.Exit(1);
                }
                if (c < 0)
                {
                    break;
                }
                switch (c)
                {
                    case '\n':
                        if (x != Width)
                        {
                            Console.Error.WriteLine("Invalid line length: " + x + "(line " + y + ")");
                            Environment.Exit(1);
                        }
                        ++y;
                        x = 0;
                        continue;
                    case '\r':
                        continue;
                    case ' ':
                        Walls[x, y] = false;
                        break;
                    case '#':
                        Walls[x, y] = true;
                        break;
                    case '1':
                        if (foundMyLocation)
                        {
                            Console.Error.WriteLine("FATAL ERROR: found two locations " +
                                                      "for player " +
                                                       "1 in the map! First location is (" +
                                                       MyLocation.X + "," +
                                                       MyLocation.Y +
                                                       "), second location is (" + x + "," +
                                                       y + ").");
                            Environment.Exit(1);
                        }
                        Walls[x, y] = true;
                        MyLocation = new Point(x, y);
                        foundMyLocation = true;
                        break;
                    case '2':
                        if (foundHisLocation)
                        {
                            Console.Error.WriteLine("FATAL ERROR: found two locations for player " +
                                           "2 in the map! First location is (" +
                                           OpponentLocation.X + "," +
                                           OpponentLocation.Y + "), second location " +
                                           "is (" + x + "," + y + ").");
                            Environment.Exit(1);
                        }
                        Walls[x, y] = true;
                        OpponentLocation = new Point(x, y);
                        foundHisLocation = true;
                        break;
                    default:
                        Console.Error.WriteLine("FATAL ERROR: invalid character received. " +
                                       "ASCII value = " + c);
                        Environment.Exit(1);
                        break;
                }
                ++x;
                ++numSpacesRead;
            }
            if (numSpacesRead != Width * Height)
            {
                Console.Error.WriteLine("FATAL ERROR: wrong number of spaces in the map. " +
                           "Should be " + (Width * Height) + ", but only found " +
                           numSpacesRead + " spaces before end of stream.");
                Environment.Exit(1);
            }
            if (!foundMyLocation)
            {
                Console.Error.WriteLine("FATAL ERROR: did not find a location for player 1!");
                Environment.Exit(1);
            }
            if (!foundHisLocation)
            {
                Console.Error.WriteLine("FATAL ERROR: did not find a location for player 2!");
                Environment.Exit(1);
            }
        }
    }
}
