using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems
{
    /// <summary>
    /// 部屋クラス
    /// </summary>
    public sealed class Room
    {
        /// <summary>
        /// 部屋を構成する全てのセル
        /// </summary>
        public readonly CellController[] Cells;

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
            var fixedSize = size - (padding * 2);
            this.Cells = new CellController[fixedSize.x * fixedSize.y];
            for (var y = 0; y < fixedSize.y; y++)
            {
                for (var x = 0; x < fixedSize.x; x++)
                {
                    var cy = start.y + (padding.y) + offset.y + y;
                    var cx = start.x + (padding.x) + offset.x + x;
                    var cell = FieldController.Cells[cy, cx];
                    var i = (fixedSize.x * y) + x;
                    this.Cells[i] = cell;
                    cell.SetCanMove(true);
                }
            }
        }
    }
}
