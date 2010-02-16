using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class Move
    {
        public Point Source;
        public Point Destination;
        public int Direction;
        public int Player;

        public Move(Point s, int dir, int player)
        {
            Source = s;
            Direction = dir;
            Destination = getNewPosition();
            Player = player;

        }

        private Point getNewPosition()
        {
            Point temp = (Point)Source.Clone();
            switch (Direction)
            {
                case 1:
                    return new Point(temp.X, temp.Y-1);
                case 2:
                    return new Point(temp.X+1, temp.Y);
                case 3:
                    return new Point(temp.X, temp.Y+1);
                case 4:
                    return new Point(temp.X-1, temp.Y);
            }
            return null;
            
        }


        public override string ToString()
        {
            if (Source == null) return null;
            return String.Format("Position: \nPlayer: {0]\nDirection: {1} \nNewPosition: {2}|{3}",Player, Direction, Destination.X, Destination.Y);
        }

    }
}
