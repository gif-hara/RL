using System.Collections.Generic;
using System.Linq;
using HK.Framework.EventSystems;
using RL.ActorControllers;
using RL.Events.FieldSystems;
using RL.Extensions;
using RL.FieldSystems.Generators.Field;
using RL.GameSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FieldController : MonoBehaviour
    {
        [SerializeField]
        private CellController cellPrefab;

        [SerializeField]
        private int matrixNumber;

        private CellController[,] cells;

        private readonly List<List<Room>> roomMatrix = new List<List<Room>>();

        private readonly List<Room> allRoom = new List<Room>();

        private float size;

        public void CreateCells()
        {
            if (this.matrixNumber <= 0)
            {
                Assert.IsTrue(false, "matrixNumberが0なためセルを生成できません");
                return;
            }

            this.cells = new CellController[this.matrixNumber, this.matrixNumber];
            var t = (RectTransform)this.transform;
            Assert.IsNotNull(t);
            this.size = (t.rect.width / this.matrixNumber);

            for (int y = 0; y < this.matrixNumber; y++)
            {
                for (int x = 0; x < this.matrixNumber; x++)
                {
                    var cell = Instantiate(this.cellPrefab, t);
                    var id = new Point(x, y);
                    cell.Initialize(id, GetPosition(id), this.size);
                    this.cells[y, x] = cell;
                }
            }
        }

        public void GenerateField(IFieldGenerator generator)
        {
            for (int y = 0; y < this.matrixNumber; y++)
            {
                for (int x = 0; x < this.matrixNumber; x++)
                {
                    this.cells[y, x].Clear();
                }
            }

            Actor.ClearEnemies();
            ClearRoom();
            generator.Generate();
            Broker.Global.Publish(GeneratedField.Get());
        }

        public static void ClearRoom()
        {
            RoomMatrix.Clear();
            AllRoom.Clear();
        }

        public static Vector2 GetPosition(Point id)
        {
            var instance = GameController.Instance.FieldController;
            return new Vector2(id.x * instance.size, -id.y * instance.size);
        }

        public static bool CanMove(Point current, Point next)
        {
            // フィールド外へ侵入しようとしてたら移動できない
            if (next.x < 0 || next.x >= XMax || next.y < 0 || next.y >= YMax)
            {
                return false;
            }

            return !IsExistCanNotMoveCell(current, next);
        }

        /// <summary>
        /// <paramref name="to"/>へ移動するためのSignされた<see cref="Point"/>を返す
        /// </summary>
        public static Point GetTargetPointSign(Point from, Point to)
        {
            for(var i = 0; i <= 4; ++i)
            {
                var result = (to - from)
                    .ToDirection();
                var left = result.RotateToLeft(i).ToPoint();
                if(CanMove(from, from + left))
                {
                    return left;
                }
                var right = result.RotateToRight(i).ToPoint();
                if(CanMove(from, from + right))
                {
                    return right;
                }
            }

            Assert.IsTrue(false, $"未定義の動作です from = {from}, to = {to}");
            return Point.Zero;
        }

        /// <summary>
        /// 自分自身の座標を含む周りのセルを返す
        /// </summary>
        /// /// <remarks>
        /// <paramref name="radius"/>が<c>1</c>の場合・・・
        /// ***
        /// *i*
        /// ***
        /// 上記のセルを返す
        /// </remarks>
        public static List<CellController> GetCellRange(Point id, int radius)
        {
            var min = new Point(
                Mathf.Max(0, id.x - radius),
                Mathf.Max(0, id.y - radius)
            );
            var max = new Point(
                Mathf.Min(XMax, id.x + radius),
                Mathf.Min(YMax, id.y + radius)
            );

            var result = new List<CellController>();
            for(var y = min.y; y < max.y; ++y)
            {
                for(var x = min.x; x < max.x; ++x)
                {
                    result.Add(Cells[y, x]);
                }
            }

            return result;
        }

        /// <summary>
        /// 自分自身の座標を含む十字方向のセルを返す
        /// </summary>
        /// /// <remarks>
        /// <paramref name="amount"/>が<c>1</c>の場合・・・
        ///  *
        /// *i*
        ///  *
        /// 上記のセルを返す
        /// </remarks>
        public static List<CellController> GetCellCross(Point id, int amount)
        {
            var min = new Point(
                Mathf.Max(0, id.x - amount),
                Mathf.Max(0, id.y - amount)
            );
            var max = new Point(
                Mathf.Min(XMax - 1, id.x + amount),
                Mathf.Min(YMax - 1, id.y + amount)
            );

            var result = new List<CellController>();
            for (var y = min.y; y <= max.y; ++y)
            {
                result.Add(Cells[y, id.x]);
            }
            for (var x = min.x; x <= max.x; ++x)
            {
                result.Add(Cells[id.y, x]);
            }

            return result;
        }

        /// <summary>
        /// <paramref name="from"/>から<paramref name="to"/>の間に移動不可能なセルが存在するか返す
        /// </summary>
        public static bool IsExistCanNotMoveCell(Point from, Point to)
        {
            var diff = to - from;
            var amount = diff.Abs;
            for (var y = 0; y <= amount.y; y++)
            {
                for (var x = 0; x <= amount.x; x++)
                {
                    var targetId = from + (diff.Vertical * y) + (diff.Horizontal * x);
                    if (!Cells[targetId.y, targetId.x].CanMove)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static float Size
        {
            get
            {
                return GameController.Instance.FieldController.size;
            }
        }

        public static CellController[,] Cells
        {
            get
            {
                return GameController.Instance.FieldController.cells;
            }
        }

        public static List<List<Room>> RoomMatrix
        {
            get
            {
                return GameController.Instance.FieldController.roomMatrix;
            }
        }

        public static List<Room> AllRoom
        {
            get
            {
                return GameController.Instance.FieldController.allRoom;
            }
        }

        public static CellController RandomCell
        {
            get
            {
                var room = AllRoom.Random();
                return room.Cells.Random();
            }
        }

        /// <summary>
        /// <see cref="Actor"/>が乗っていないセルを返す
        /// </summary>
        public static CellController RandomCellNotRideActor
        {
            get
            {
                // Actorで埋まっていない部屋を取得する
                var possibleRooms = AllRoom.Where(r => r.ExistActorNumber != r.Cells.Length);
                Assert.AreNotEqual(possibleRooms.Count(), 0, $"{typeof(Actor).Name}が存在しない{typeof(Room).Name}の取得に失敗しました　");

                var room = possibleRooms.Random();
                var cells = room.Cells.Where(c => c.RideActor == null);
                return cells.Random();
            }
        }

        public static Point RandomPosition
        {
            get
            {
                return RandomCell.Id;
            }
        }

        public static int XMax
        {
            get
            {
                return GameController.Instance.FieldController.matrixNumber;
            }
        }

        public static int YMax
        {
            get
            {
                return GameController.Instance.FieldController.matrixNumber;
            }
        }
    }
}
