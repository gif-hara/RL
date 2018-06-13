using System;
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
        public readonly Point Id;

        /// <summary>
        /// 部屋を構成する全てのセル
        /// </summary>
        public readonly CellController[] Cells;

        public readonly Edges Edges;

        public Room(Point id, Point start, Point size, Point paddingMin, Point paddingMax, Point offsetSize)
        {
            this.Id = id;
            
            // TODO: RoomGenerator的なクラスに生成させたい
            var padding = new Point(
                UnityEngine.Random.Range(paddingMin.x, paddingMax.x + 1),
                UnityEngine.Random.Range(paddingMin.y, paddingMax.y + 1)
            );
            var offset = new Point(
                UnityEngine.Random.Range(-offsetSize.x, offsetSize.x + 1),
                UnityEngine.Random.Range(-offsetSize.y, offsetSize.y + 1)
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

            this.Edges = new Edges(this.Cells);
        }

        /// <summary>
        /// この部屋に存在する<see cref="Actor"/>の数を返す
        /// </summary>
        /// <returns></returns>
        public int ExistActorNumber
        {
            get
            {
                var result = 0;
                for(var i = 0; i < this.Cells.Length; ++i)
                {
                    result += this.Cells[i].RideActor == null ? 0 : 1;
                }

                return result;
            }
        }
    }
}
