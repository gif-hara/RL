using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems.FieldSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Room
    {
        public readonly List<CellController> Cells = new List<CellController>();

        public Room(Point start, Point size, Point paddingMin, Point paddingMax)
        {
            // RoomGenerator的なクラスに生成させたい
            var paddingX = Random.Range(paddingMin.x, paddingMax.x);
            var paddingY = Random.Range(paddingMin.y, paddingMax.y);
            var offsetX = Random.Range(0, paddingX);
            var offsetY = Random.Range(0, paddingY);
            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    var canMove =
                        x >= paddingX && x < (size.x - paddingX)
                        && y >= paddingY && y < (size.y - paddingY);
                    if (!canMove)
                    {
                        continue;
                    }

                    var cell = FieldController.Cells[start.y + offsetY + y, start.x + offsetX + x];
                    this.Cells.Add(cell);
                    cell.SetCanMove(canMove);
                }
            }
        }
    }
}
