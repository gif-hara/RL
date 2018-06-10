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
    }
}
