using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Room
    {
        public readonly List<CellController> Cells = new List<CellController>();

        public Room(Point start, Point size, Point paddingMin, Point paddingMax, Point offsetSize)
        {
            // RoomGenerator的なクラスに生成させたい
            var padding = new Point(
                Random.Range(paddingMin.x, paddingMax.x + 1),
                Random.Range(paddingMin.y, paddingMax.y + 1)
            );
            var offset = new Point(
                Random.Range(-offsetSize.x, offsetSize.x + 1),
                Random.Range(-offsetSize.y, offsetSize.y + 1)
            );
            for (var y = 0; y < (size.y - padding.y * 2); y++)
            {
                for (var x = 0; x < (size.x - padding.x * 2); x++)
                {
                    var cy = start.y + (padding.y) + offset.y + y;
                    var cx = start.x + (padding.x) + offset.x + x;
                    var cell = FieldController.Cells[cy, cx];
                    this.Cells.Add(cell);
                    cell.SetCanMove(true);
                }
            }
        }
    }
}
