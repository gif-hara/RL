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
                    var h = x + 1;
                    if(h < rooms.Count)
                    {
                        this.Generate(rooms[x], rooms[h]);
                    }

                    // 垂直方向の通路を作成する
                    var v = y + 1;
                    if(v < roomMatrix.Count)
                    {
                        var vRooms = roomMatrix[v];
                        if(x < vRooms.Count)
                        {
                            this.Generate(rooms[x], vRooms[x]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 部屋と部屋を繋ぐ通路を作成する
        /// </summary>
        /// <remarks>
        /// <paramref name="a"/>から<paramref name="b"/>の方向を算出してそれぞれの側面を取得する
        /// 側面同士の中央点を算出したあと始点と終点を同時に通路を作成して中央点で繋げるようにしている
        /// </remarks>
        private void Generate(Room a, Room b)
        {
            // aからbへの方向、bからaへの方向を算出する
            var direction = (b.Id - a.Id).ToDirection().ToCross();
            var invertDirection = direction.Invert();

            // aとbの側面を取得する
            var aEdge = a.Edges.GetFromDirection(direction);
            var bEdge = b.Edges.GetFromDirection(invertDirection);

            // aとbの通路地点（始点と終点）を算出する
            var startId = aEdge.RandomCell.Id;
            var endId = bEdge.RandomCell.Id;

            // 始点と終点の中央点を算出する
            var centerId = (startId + endId) / 2;

            // 始点から通路を作成
            this.Generate(a, startId, centerId, direction);

            // 終点から通路を作成
            this.Generate(b, endId, centerId, invertDirection);
        }

        /// <summary>
        /// <paramref name="to"/>までの通路を作成する
        /// </summary>
        private void Generate(Room room, Point from, Point to, Direction direction)
        {
            // 最初のセルを入り口とする
            room.Entrances.Add(new Entrance(FieldController.Cells[from.y, from.x], direction));

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
