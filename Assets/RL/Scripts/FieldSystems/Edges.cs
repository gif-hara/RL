using System;
using RL.Extensions;
using RL.GameSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems
{
    /// <summary>
    /// <see cref="Room"/>の全ての端を保持するクラス
    /// </summary>
    public class Edges
    {
        public readonly Edge Left;

        public readonly Edge Top;

        public readonly Edge Right;

        public readonly Edge Bottom;

        public Edges(CellController[] cells)
        {
            var min = Point.MaxValue;
            var max = Point.Zero;
            for (var i = 0; i < cells.Length; i++)
            {
                var cell = cells[i];
                min.x = Mathf.Min(min.x, cell.X);
                min.y = Mathf.Min(min.y, cell.Y);
                max.x = Mathf.Max(max.x, cell.X);
                max.y = Mathf.Max(max.y, cell.Y);
            }

            this.Left = new Edge(Array.FindAll(cells, c => c.X == min.x));
            this.Top = new Edge(Array.FindAll(cells, c => c.Y == min.y));
            this.Right = new Edge(Array.FindAll(cells, c => c.X == max.x));
            this.Bottom = new Edge(Array.FindAll(cells, c => c.Y == max.y));
        }

        public Edge GetFromDirection(Direction direction)
        {
            Assert.IsTrue(direction.IsCross());

            switch(direction)
            {
                case Direction.Left:
                    return this.Left;
                case Direction.Top:
                    return this.Top;
                case Direction.Right:
                    return this.Right;
                case Direction.Bottom:
                    return this.Bottom;
                default:
                    Assert.IsTrue(false, $"未対応の値です direction = {direction}");
                    return null;
            }
        }
    }

    public class Edge
    {
        public readonly CellController[] Cells;

        public Edge(CellController[] cells)
        {
            this.Cells = cells;
        }

        public CellController RandomCell
        {
            get
            {
                return this.Cells[UnityEngine.Random.Range(0, this.Cells.Length)];
            }
        }
    }
}
