using RL.Extensions;
using RL.GameSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.Generators.Aisle
{
    /// <summary>
    /// 通常の通路を生成する
    /// </summary>
    [CreateAssetMenu(menuName = "RL/FieldSystem/Generator/Aisle/Basic")]
    public sealed class BasicScriptableAisleGenerator : ScriptableAisleGenerator
    {
        public override void Generate()
        {
            var roomMatrix = FieldController.RoomMatrix;
            for (var y = 0; y < roomMatrix.Count; y++)
            {
                var rooms = roomMatrix[y];
                for (var x = 0; x < rooms.Count; x++)
                {
                    // 水平方向の通路を作成する
                    var i = x;
                    var k = x + 1;
                    if(k < rooms.Count)
                    {
                        this.Generate(rooms[i], rooms[k]);
                    }

                    // 垂直方向の通路を作成する
                    var nextY = y + 1;
                    if(nextY < roomMatrix.Count)
                    {
                        var nextRooms = roomMatrix[nextY];
                        if(x < nextRooms.Count)
                        {
                            this.Generate(rooms[i], nextRooms[x]);
                        }
                    }
                }
            }
        }

        private void Generate(Room a, Room b)
        {
            // aからbへの通路の開始方向を算出する
            var direction = (b.Id - a.Id).ToDirection().ToCross();
            var invertDirection = direction.Invert();
            var startEdge = a.Edges.GetFromDirection(direction);
            var endEdge = b.Edges.GetFromDirection(invertDirection);
            var startId = startEdge.RandomCell.Id;
            var endId = endEdge.RandomCell.Id;
            var centerId = (startId + endId) / 2;
            this.SetCanMove(startId, centerId, direction);
            this.SetCanMove(endId, centerId, invertDirection);
        }

        private void SetCanMove(Point from, Point to, Direction direction)
        {
            while(from != to)
            {
                from += direction.ToPoint();
                FieldController.Cells[from.y, from.x].SetCanMove(true);
                var diff = to - from;
                if (direction.IsHorizontal())
                {
                    if (diff.x == 0)
                    {
                        direction = diff.ToDirection();
                    }
                }
                else if (direction.IsVertical())
                {
                    if (diff.y == 0)
                    {
                        direction = diff.ToDirection();
                    }
                }
                else
                {
                    Assert.IsTrue(false, $"未対応の挙動です direction = {direction}");
                    break;
                }
            }
        }
    }
}
