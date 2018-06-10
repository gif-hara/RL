using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL
{
    [Serializable]
    public struct Point
    {
        public int x;

        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.x + right.x, left.y + right.y);
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.x - right.x, left.y - right.y);
        }

        public static Point operator *(Point a, int m)
        {
            return new Point(a.x * m, a.y * m);
        }

        public static Point operator *(Point a, float m)
        {
            return new Point((int)(a.x * m), (int)(a.y * m));
        }
    }
}
