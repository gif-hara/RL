using System;
using RL.Extensions;
using RL.GameSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL
{
    [Serializable]
    public struct Point
    {
        public int x;

        public int y;

        public static Point Zero
        {
            get
            {
                return new Point(0, 0);
            }
        }

        public static Point MaxValue
        {
            get
            {
                return new Point(int.MaxValue, int.MaxValue);
            }
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point Abs
        {
            get
            {
                return new Point(Mathf.Abs(this.x), Mathf.Abs(this.y));
            }
        }

        /// <summary>
        /// 水平方向の要素を返す
        /// </summary>
        public Point Horizontal
        {
            get
            {
                return new Point(this.x, 0);
            }
        }

        /// <summary>
        /// 垂直方向の要素を返す
        /// </summary>
        public Point Vertical
        {
            get
            {
                return new Point(0, this.y);
            }
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

        public static Point operator /(Point a, int m)
        {
            return new Point(a.x / m, a.y / m);
        }

        public static Point operator /(Point a, float m)
        {
            return new Point((int)(a.x / m), (int)(a.y / m));
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Point))
            {
                return false;
            }

            return this == (Point)obj;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode();
        }

        public override string ToString()
        {
            return $"(x = {this.x} y = {this.y})";
        }
    }
}
